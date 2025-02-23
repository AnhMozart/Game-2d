using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 12f;
    [SerializeField] private float Damage = 10f;
    [SerializeField] private GameObject blood; //Hiệu ứng nổ khi va trạm vào kẻ địch


    private void Update()
    {
        BulletMoving();
    }


    private void BulletMoving()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamege(Damage);
            }
            FindFirstObjectByType<ObjectPool>().ReturnGameObject(gameObject);
            Bloom();
        }
    }


    private void Bloom()
    {
        if(blood != null)
        {
            GameObject Blooms = Instantiate(blood,transform.position,Quaternion.identity);
            Destroy(Blooms,1f);
        }    
    }
    
}
