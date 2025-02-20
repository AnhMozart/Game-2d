using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rgb;
    [SerializeField] float MoveSpeed = 1f;
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rgb.velocity = new Vector2(MoveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        MoveSpeed = -MoveSpeed;
        FlipEnemy();
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rgb.velocity.x)), 1f);
    }


}
