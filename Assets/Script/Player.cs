using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rgb;
    [SerializeField] private float Jump = 1.5f;
    [SerializeField] private float rotationJump = 10f;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            rgb.velocity = Vector2.up * Jump;
            AudioManager.instance.PlayJumpSound();
        }
    }


    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rgb.velocity.y * rotationJump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("obstruction"))
        {
            GameManager.instans.GameOver();
            AudioManager.instance.PlayDieSound();
        }
        else if(collision.CompareTag("point"))
        {
            GameManager.instans.Addpoint();
            AudioManager.instance.PlayCoinSound();
        }    
    }
}
