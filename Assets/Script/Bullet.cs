using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float BulletSpeed = 20f;
    Rigidbody2D rgb;
    PlayerMovement player;
    float xspeed;
    [SerializeField] AudioClip vachamtuong;
    [SerializeField] AudioClip trungkedich;
    int poinEnemy = 100;


    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        player =FindObjectOfType<PlayerMovement>();
        xspeed = player.transform.localScale.x * BulletSpeed;
    }

    void Update()
    {
        rgb.velocity = new Vector2(xspeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource.PlayClipAtPoint(trungkedich, Camera.main.transform.position);
        if (other.tag == "enemy,Boss")
        {
            // Destroy the enemy
            Destroy(other.gameObject);
            FindObjectOfType<GameSession>().addScore(poinEnemy);

        }
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(vachamtuong, Camera.main.transform.position);
        Destroy(gameObject);

    }
}
