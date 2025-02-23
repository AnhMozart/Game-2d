using UnityEngine;

public class Boss_1 : Enemy
{
    [Header("Bullet_Infor")]
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float circularBulletSpeed = 10f;

    [Space(2)]
    [SerializeField] private float HP_Heal = 20f;

    [Space(1)]
    [SerializeField] private GameObject bossPetPrefab;

    [SerializeField] private float skillCooldown = 2f;
    private float nextSkill;

    [SerializeField] private GameObject MilkPrefab;



    protected override void Update()
    {
        base.Update();
        UseSkillBoss();
    }


    protected override void Die()
    {
        base.Die();
        Instantiate(MilkPrefab, transform.position, Quaternion.identity);
        GameManager.instance.Winner();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damagebasic);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damageStay);
        }
    }

    private void ShootingGun()
    {
        if (player != null)
        {
            Vector3 playerDirection = player.transform.position - firePos.position;  //lấy hướng của người chơi đến firePst
            playerDirection.Normalize(); // huyển đổi vector playerDirection thành vector đơn vị (unit vector). 

            GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity); //Nhân bản ra viên đạn

            Bullet_Boss bullet_Boss = bullet.AddComponent<Bullet_Boss>();  //Thêm Bulletboss vào bullet và bullet_boss sẽ lấy đươc các thành phần ở Bulllet_Boss
            bullet_Boss.SetDirection(playerDirection * bulletSpeed);
            AudioManager.instance.PlayShootSound();
        }
    }


    private void CircularBullet()
    {
        const int Bullet_Munber = 12; //Hằng số không thay đổi
        float angleStep = 360 / Bullet_Munber; //khoảng cách góc giữa mỗi viên đạn

        for (int i = 0; i < Bullet_Munber; i++)
        {
            float angle = angleStep * i; //góc của viên đạn thứ i 
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet_Boss bullet_Boss = bullet.AddComponent<Bullet_Boss>();
            bullet_Boss.SetDirection(bulletDirection * circularBulletSpeed);
            AudioManager.instance.PlayShootSound();
        }

    }

    private void Heal_Boss(float _heal)  //Hồi máu cho boss
    {
        currrentHP = Mathf.Min(currrentHP + _heal, maxHP);
        UpdateHpBar();
    }    


    private void CallPet()
    {
        Instantiate(bossPetPrefab, transform.position, Quaternion.identity);
    }


    private void Teleport() 
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        } 
            
    }


    private void UseSkillNumber() // sử dụng skill thứ 1-5
    {
        float Skill_Number = Random.Range(1,5);
        nextSkill = Time.time + skillCooldown;
        switch(Skill_Number)
        {
            case 1:
                ShootingGun();
                break;
            case 2:
                CircularBullet();
                break;
            case 3:
                Heal_Boss(HP_Heal);
                break;
            case 4:
                CallPet();
                break;
            case 5:
                Teleport();
                break;
        }    
    }


    private void UseSkillBoss()
    {
        if(nextSkill < Time.time)
        {
            UseSkillNumber();
        }    
    }    
    
}
