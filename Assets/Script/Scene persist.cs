using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenepersist : MonoBehaviour
{
    void Awake()
    {
        // 1. Kiểm tra xem có bao nhiêu phiên bản GameSession đang tồn tại trong cảnh (scene).
        int numScenePersist = FindObjectsOfType<Scenepersist>().Length;
        // 2. Nếu đã có hơn 1 GameSession (khi quay lại một scene khác hoặc reload scene), thì phá hủy đối tượng hiện tại.
        if (numScenePersist > 1)
        {
            Destroy(gameObject);// Hủy đối tượng hiện tại vì đã có GameSession khác tồn tại.
        }
        else
        {
            // 3. Nếu không có GameSession khác, đối tượng này sẽ được giữ lại khi tải màn chơi mới.
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
