using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float rotateoffset = 180f; // Góc offset (bù) để điều chỉnh hướng của súng 

    [Header("Bullet")]
    [SerializeField] private Transform firePos; //vi chi ban ra vien dan
    [SerializeField] private GameObject bulletPrefab; // vien dan
    [SerializeField] private float shotDelay; // khoảng thời gian chờ giữa 2 lần bắn
    private float nextShot; //Thời gian lần bắn tiếp theo
    [SerializeField] private int maxAmmo = 24;
    public int currentAmmo;

    [SerializeField] private TextMeshProUGUI Number_Bullets;
    [SerializeField] public Joystick joystickGun;

    [SerializeField] private ObjectPool pool;



    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateNumberBullets();
    }


    private void Update()
    {
        RotateGun(); // Gọi hàm xoay súng mỗi frame
        Shoot();
        ReloadGun();
    }


    private void RotateGun()
    {
        // Kiểm tra xem chuột có nằm ngoài màn hình không. Nếu có, không thực hiện xoay súng.
        // Điều này để tránh các lỗi tiềm ẩn khi chuột ra khỏi màn hình.
        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height) return;

        // Tính toán vector chỉ hướng từ vị trí súng đến vị trí chuột.
        // Camera.main.ScreenToWorldPoint(Input.mousePosition) chuyển đổi vị trí chuột trên màn hình sang vị trí trong không gian thế giới.
        // transform.position là vị trí của súng trong không gian thế giới.
        Vector3 displancement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Tính góc giữa vector chỉ hướng và trục x (tính bằng độ).
        // Mathf.Atan2(displancement.y, displancement.x) trả về góc (radian) giữa vector và trục x.
        // * Mathf.Rad2Deg chuyển đổi radian sang độ.
        float angle = Mathf.Atan2(displancement.y, displancement.x) * Mathf.Rad2Deg;

        // Xoay súng theo góc đã tính + góc offset.  Quaternion.Euler tạo một quaternion (biểu diễn phép xoay) từ góc Euler (trong trường hợp này chỉ có góc z).
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateoffset);

        // Lật súng nếu góc lớn hơn 90 độ hoặc nhỏ hơn -90 độ.
        // Mục đích là để súng luôn hướng theo con chuột một cách trực quan, không bị lộn ngược.
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, 1, 1); // Không lật (giữ nguyên scale)
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1); // Lật theo trục y (scale y = -1)
        }
    }


    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = shotDelay + Time.time;
            //Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
            GameObject bulletPool = pool.GetObject();
            bulletPool.transform.position = firePos.position;
            bulletPool.transform.rotation = firePos.rotation;
            AudioManager.instance.PlayShootSound();
            StartCoroutine(returnBullet(bulletPool));
            currentAmmo--;
            UpdateNumberBullets();
        }
    }


    IEnumerator returnBullet(GameObject gameObject)
    {
        yield return new WaitForSeconds(2f);
        FindFirstObjectByType<ObjectPool>().ReturnGameObject(gameObject);
    }

    private void ReloadGun()
    {
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            UpdateNumberBullets();
            AudioManager.instance.PlayReloadSound();
        }
    }



    /*    private void RotateGun()
        {
            if (joystickGun == null) return;

            float horizontalInput = joystickGun.Horizontal;
            float verticalInput = joystickGun.Vertical;

            if (horizontalInput != 0 || verticalInput != 0)
            {
                // Tính góc của joystick
                float joystickAngle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;

                // Áp dụng góc xoay cho súng (KHÔNG sử dụng rotateOffset)
                transform.rotation = Quaternion.Euler(0, 0, joystickAngle);

                // Lật súng nếu cần
                transform.localScale = new Vector3(1, Mathf.Abs(joystickAngle) < 90 ? 1 : -1, 1);
            }
        }



        private void Shoot()
        {
            if (joystickGun == null) return;

            float horizontalInput = joystickGun.Horizontal;
            float verticalInput = joystickGun.Vertical;

            if ((horizontalInput != 0 || verticalInput != 0) && currentAmmo > 0 && Time.time > nextShot)
            {
                nextShot = shotDelay + Time.time;
                Instantiate(bulletPrefab, firePos.position, firePos.rotation);
                AudioManager.instance.PlayShootSound();
                currentAmmo--;
                UpdateNumberBullets();
            }
        }


        private void ReloadGun()
        {
            if (currentAmmo <= 0)
            {
                currentAmmo = maxAmmo;
                UpdateNumberBullets();
                AudioManager.instance.PlayReloadSound();
            }
        }
    */

    private void UpdateNumberBullets()
    {
        if(Number_Bullets !=  null)
        {
            if (currentAmmo > 0)
            {
                Number_Bullets.text = currentAmmo.ToString();
            }
            else
            {
                Number_Bullets.text = "Emty";
            }

        }

    }
}