using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace GameData
{
    // Class để lưu thông tin bảng xếp hạng
    public class PlayerLeaderboardEntry
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public float Score { get; set; }  // Đổi từ int sang float vì coin là float
        public string Timestamp { get; set; }
    }

    public class PlayerDataManager : MonoBehaviour
    {
        public static PlayerDataManager Instance { get; private set; }
        private DatabaseReference dbReference;
        private FirebaseAuth auth;

        // Cache dữ liệu người chơi
        private float cachedCoin = 0f;
        private int cachedUnlockedLevel = 1;
        private bool isInMainMenu = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeFirebase();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Đăng ký sự kiện khi chuyển scene
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void InitializeFirebase()
        {
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            auth = FirebaseAuth.DefaultInstance;

            // Đăng ký sự kiện thay đổi trạng thái đăng nhập
            if (auth != null)
            {
                auth.StateChanged += Auth_StateChanged;
            }
        }

        private void OnDestroy()
        {
            // Hủy đăng ký sự kiện khi destroy
            if (auth != null)
            {
                auth.StateChanged -= Auth_StateChanged;
            }
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            isInMainMenu = scene.name.Contains("MainMenu");
            Debug.Log($"Loaded scene: {scene.name}, isInMainMenu: {isInMainMenu}");

            // Nếu chuyển từ MainMenu sang Game scene, chuyển dữ liệu từ cache sang
            if (!isInMainMenu && auth != null && auth.CurrentUser != null)
            {
                // Đợi một frame để đảm bảo các Manager đã được khởi tạo
                StartCoroutine(TransferDataAfterSceneLoad());
            }
        }

        private System.Collections.IEnumerator TransferDataAfterSceneLoad()
        {
            // Đợi 2 frames để đảm bảo mọi thứ đã được khởi tạo
            yield return null;
            yield return null;

            TransferCachedDataToGame();
        }

        private async void Auth_StateChanged(object sender, System.EventArgs e)
        {
            Debug.Log("Auth_StateChanged được gọi");
            
            if (auth.CurrentUser != null)
            {
                // Người dùng vừa đăng nhập
                Debug.Log($"Người dùng đăng nhập: {auth.CurrentUser.Email}");
                
                // Load dữ liệu ngay lập tức
                await InitializeUserData();
            }
            else
            {
                // Người dùng đăng xuất - reset data
                Debug.Log("Người dùng đăng xuất - Bắt đầu reset data");
                await ForceResetAllData();
                Debug.Log("Đã reset data sau khi đăng xuất");
            }
        }

        private async Task ForceResetAllData()
        {
            Debug.Log("Bắt đầu reset toàn bộ dữ liệu...");

            try
            {
                // Reset cache trước
                cachedCoin = 0f;
                cachedUnlockedLevel = 1;
                Debug.Log("Đã reset cache data");

                // Đợi 1 frame
                await Task.Yield();

                // Reset PlayerManager và LevelManager
                if (PlayerManager.instance != null)
                {
                    PlayerManager.instance.SetCoin(0);
                    Debug.Log("Đã reset coin trong PlayerManager");
                }
                else
                {
                    Debug.LogWarning("PlayerManager không tồn tại khi reset");
                }

                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.SetUnlockedLevel(1);
                    Debug.Log("Đã reset level trong LevelManager");
                }
                else
                {
                    Debug.LogWarning("LevelManager không tồn tại khi reset");
                }

                // Đợi thêm 1 frame để đảm bảo mọi thứ được cập nhật
                await Task.Yield();

                Debug.Log("Reset toàn bộ dữ liệu hoàn tất");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Lỗi khi reset toàn bộ dữ liệu: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private async Task InitializeUserData()
        {
            try
            {
                Debug.Log("Bắt đầu khởi tạo dữ liệu người dùng...");

                // Đợi 1 frame trước khi load
                await Task.Yield();

                // Kiểm tra dữ liệu hiện có trên Firebase
                string userId = auth.CurrentUser.UserId;
                var snapshot = await dbReference.Child("players").Child(userId).GetValueAsync();

                if (snapshot != null && snapshot.Exists)
                {
                    // Nếu có dữ liệu trên Firebase, load về
                    // Đợi thêm 1 frame
                    await Task.Yield();

                    // Đảm bảo dữ liệu được chuyển sang game nếu cần
                    if (!isInMainMenu)
                    {
                        TransferCachedDataToGame();
                    }

                    // Tải bảng xếp hạng
                    var leaderboard = await GetTopPlayers();

                    Debug.Log($"Khởi tạo dữ liệu hoàn tất - Coin: {cachedCoin}, Level: {cachedUnlockedLevel}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Lỗi khi khởi tạo dữ liệu: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        // Lưu dữ liệu người chơi
        public async Task SavePlayerData()
        {
            if (!IsSignedIn())
            {
                Debug.LogWarning("Không thể lưu dữ liệu: Người dùng chưa đăng nhập");
                return;
            }

            try
            {
                Debug.Log("Bắt đầu lưu dữ liệu người chơi...");

                // Lấy dữ liệu hiện tại
                float currentCoin;
                int currentLevel;

                if (!isInMainMenu && PlayerManager.instance != null && LevelManager.Instance != null)
                {
                    currentCoin = PlayerManager.instance.GetCurrentCoin();
                    currentLevel = LevelManager.Instance.GetCurrentUnlockedLevel();
                    Debug.Log($"Lấy dữ liệu từ Manager: Coin={currentCoin}, Level={currentLevel}");
                }
                else
                {
                    currentCoin = cachedCoin;
                    currentLevel = cachedUnlockedLevel;
                    Debug.Log($"Lấy dữ liệu từ cache: Coin={currentCoin}, Level={currentLevel}");
                }

                string userId = auth.CurrentUser.UserId;
                string displayName = auth.CurrentUser.DisplayName ?? auth.CurrentUser.Email?.Split('@')[0] ?? "Anonymous";

                var playerData = new Dictionary<string, object>
                {
                    { "coin", currentCoin },
                    { "unlockedLevel", currentLevel },
                    { "displayName", displayName },
                    { "lastUpdated", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
                };

                Debug.Log($"Chuẩn bị lưu dữ liệu: {string.Join(", ", playerData.Select(kv => $"{kv.Key}={kv.Value}"))}");

                // Cập nhật dữ liệu người chơi
                await dbReference.Child("players").Child(userId).UpdateChildrenAsync(playerData);

                // Cập nhật bảng xếp hạng
                await UpdateLeaderboard(currentCoin);

                // Cập nhật cache
                cachedCoin = currentCoin;
                cachedUnlockedLevel = currentLevel;

                Debug.Log($"Đã lưu dữ liệu thành công: Coin={currentCoin}, Level={currentLevel}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Lỗi khi lưu dữ liệu: {e.Message}\nStackTrace: {e.StackTrace}");
            }
        }

        // Cập nhật bảng xếp hạng
        private async Task UpdateLeaderboard(float currentCoin)
        {
            if (!IsSignedIn()) return;

            try
            {
                string userId = auth.CurrentUser.UserId;
                var leaderboardEntry = new Dictionary<string, object>
                {
                    { "userId", userId },
                    { "displayName", auth.CurrentUser.DisplayName ?? auth.CurrentUser.Email?.Split('@')[0] ?? "Anonymous" },
                    { "coin", currentCoin },
                    { "timestamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
                };

                await dbReference.Child("leaderboard").Child(userId).UpdateChildrenAsync(leaderboardEntry);
            }
            catch (Exception e)
            {
                Debug.LogError($"Lỗi khi cập nhật bảng xếp hạng: {e.Message}");
            }
        }

        // Phương thức mới để chuyển dữ liệu từ cache sang PlayerManager khi vào game
        public void TransferCachedDataToGame()
        {
            try
            {
                Debug.Log($"Bắt đầu chuyển dữ liệu cache sang game - Cache: Coin={cachedCoin}, Level={cachedUnlockedLevel}");

                if (PlayerManager.instance != null)
                {
                    PlayerManager.instance.SetCoin(cachedCoin);
                    Debug.Log($"Đã chuyển coin={cachedCoin} sang PlayerManager");
                }
                else
                {
                    Debug.LogError("Không thể chuyển dữ liệu: PlayerManager chưa được khởi tạo");
                }

                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.SetUnlockedLevel(cachedUnlockedLevel);
                    Debug.Log($"Đã chuyển level={cachedUnlockedLevel} sang LevelManager");
                }
                else
                {
                    Debug.LogError("Không thể chuyển dữ liệu: LevelManager chưa được khởi tạo");
                }

                Debug.Log("Hoàn tất chuyển dữ liệu cache sang game");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Lỗi khi chuyển dữ liệu: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        public async Task LoadPlayerData()
        {
            if (!IsSignedIn())
            {
                Debug.LogWarning("Không thể tải dữ liệu: Người dùng chưa đăng nhập");
                return;
            }

            try
            {
                Debug.Log("Bắt đầu tải dữ liệu người chơi...");
                string userId = auth.CurrentUser.UserId;

                // Load dữ liệu local trước
                float localCoin = PlayerPrefs.GetFloat("CurrentCoin", 0);
                int localLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
                Debug.Log($"Dữ liệu local: Coin={localCoin}, Level={localLevel}");

                // Load dữ liệu từ Firebase
                var snapshot = await dbReference.Child("players").Child(userId).GetValueAsync();
                
                if (snapshot != null && snapshot.Exists)
                {
                    var data = snapshot.Value as Dictionary<string, object>;
                    if (data != null)
                    {
                        float firebaseCoin = GetSafeFloat(data, "coin");
                        int firebaseLevel = GetSafeInt(data, "unlockedLevel", 1);
                        Debug.Log($"Dữ liệu từ Firebase: Coin={firebaseCoin}, Level={firebaseLevel}");

                        // Sử dụng giá trị lớn nhất
                        cachedCoin = Mathf.Max(firebaseCoin, localCoin);
                        cachedUnlockedLevel = Mathf.Max(firebaseLevel, localLevel);
                        Debug.Log($"Sau khi merge: Coin={cachedCoin}, Level={cachedUnlockedLevel}");

                        // Nếu dữ liệu local lớn hơn, cập nhật lên Firebase
                        if (localCoin > firebaseCoin || localLevel > firebaseLevel)
                        {
                            await SavePlayerData();
                            Debug.Log("Đã cập nhật dữ liệu lớn hơn lên Firebase");
                        }
                    }
                }
                else
                {
                    // Nếu không có dữ liệu trên Firebase, sử dụng dữ liệu local
                    Debug.Log("Không có dữ liệu trên Firebase, sử dụng dữ liệu local");
                    cachedCoin = localCoin;
                    cachedUnlockedLevel = localLevel;

                    // Lưu dữ liệu local lên Firebase nếu có
                    if (localCoin > 0 || localLevel > 1)
                    {
                        await SavePlayerData();
                        Debug.Log("Đã lưu dữ liệu local lên Firebase");
                    }
                }

                // Cập nhật các Manager
                if (!isInMainMenu)
                {
                    if (PlayerManager.instance != null)
                    {
                        PlayerManager.instance.SetCoin(cachedCoin);
                        Debug.Log($"Đã cập nhật coin trong PlayerManager: {cachedCoin}");
                    }

                    if (LevelManager.Instance != null)
                    {
                        LevelManager.Instance.SetUnlockedLevel(cachedUnlockedLevel);
                        Debug.Log($"Đã cập nhật level trong LevelManager: {cachedUnlockedLevel}");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Lỗi khi tải dữ liệu: {e.Message}\nStackTrace: {e.StackTrace}");
                // Trong trường hợp lỗi, vẫn sử dụng dữ liệu local
                cachedCoin = PlayerPrefs.GetFloat("CurrentCoin", 0);
                cachedUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            }
        }

        private float GetSafeFloat(Dictionary<string, object> data, string key, float defaultValue = 0f)
        {
            if (data != null && data.ContainsKey(key) && data[key] != null)
            {
                try
                {
                    return Convert.ToSingle(data[key]);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Lỗi khi chuyển đổi {key}: {ex.Message}");
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        private int GetSafeInt(Dictionary<string, object> data, string key, int defaultValue = 0)
        {
            if (data != null && data.ContainsKey(key) && data[key] != null)
            {
                try
                {
                    return Convert.ToInt32(data[key]);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Lỗi khi chuyển đổi {key}: {ex.Message}");
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        // Lấy top 10 người chơi có số coin cao nhất
        public async Task<List<PlayerLeaderboardEntry>> GetTopPlayers(int limit = 10)
        {
            var leaderboard = new List<PlayerLeaderboardEntry>();

            try
            {
                var snapshot = await dbReference.Child("leaderboard")
                    .OrderByChild("coin")
                    .LimitToLast(limit)
                    .GetValueAsync();

                Debug.Log($"Đã nhận snapshot: Exists={snapshot?.Exists}, ChildrenCount={snapshot?.ChildrenCount}");

                if (snapshot != null && snapshot.Exists)
                {
                    // Xử lý từng child node thay vì ép kiểu trực tiếp
                    foreach (var childSnapshot in snapshot.Children)
                    {
                        try
                        {
                            var data = childSnapshot.Value as Dictionary<string, object>;
                            if (data == null)
                            {
                                Debug.LogWarning($"Data null cho child key: {childSnapshot.Key}");
                                continue;
                            }

                            Debug.Log($"Xử lý entry: {string.Join(", ", data.Select(kv => $"{kv.Key}={kv.Value}"))}");

                            string userId = GetSafeString(data, "userId");
                            string displayName = GetSafeString(data, "displayName");
                            float score = GetSafeFloat(data, "coin");
                            string timestamp = GetSafeString(data, "timestamp", DateTime.Now.ToString());

                            // Nếu displayName trống, thử lấy từ email
                            if (string.IsNullOrEmpty(displayName) && auth.CurrentUser != null && userId == auth.CurrentUser.UserId)
                            {
                                displayName = auth.CurrentUser.Email?.Split('@')[0] ?? "Anonymous";
                            }

                            var entry = new PlayerLeaderboardEntry
                            {
                                UserId = userId,
                                DisplayName = displayName,
                                Score = score,
                                Timestamp = timestamp
                            };

                            Debug.Log($"Đã xử lý entry: UserId={entry.UserId}, DisplayName={entry.DisplayName}, Score={entry.Score}");
                            leaderboard.Add(entry);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Lỗi khi xử lý entry: {ex.Message}\nStackTrace: {ex.StackTrace}");
                            continue;
                        }
                    }

                    // Sắp xếp theo điểm số giảm dần
                    leaderboard.Sort((a, b) => b.Score.CompareTo(a.Score));

                    Debug.Log($"Tổng số entry đã xử lý: {leaderboard.Count}");
                    foreach (var entry in leaderboard)
                    {
                        Debug.Log($"Entry trong bảng xếp hạng: {entry.DisplayName} - {entry.Score} điểm");
                    }
                }
                else
                {
                    Debug.LogWarning("Không tìm thấy dữ liệu bảng xếp hạng!");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Lỗi khi lấy bảng xếp hạng: {e.Message}\nStackTrace: {e.StackTrace}");
            }

            return leaderboard;
        }

        private string GetSafeString(Dictionary<string, object> data, string key, string defaultValue = "")
        {
            if (data != null && data.ContainsKey(key) && data[key] != null)
            {
                return data[key].ToString();
            }
            return defaultValue;
        }

        // Thêm các phương thức để lấy dữ liệu đã cache
        public float GetCachedCoin() => cachedCoin;
        public int GetCachedUnlockedLevel() => cachedUnlockedLevel;

        public bool IsSignedIn()
        {
            return auth != null && auth.CurrentUser != null;
        }

        // Thêm phương thức để cập nhật dữ liệu khi cần
        public async Task RefreshUserData()
        {
            if (IsSignedIn())
            {
                await InitializeUserData();
            }
        }
    }
}