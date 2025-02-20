using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float Tocdochay = 10f;
    [SerializeField] float JumpSpeed = 5f;
    [SerializeField] float tocdoleo = 5f;
    [SerializeField] public Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform Gun;
    Vector2 moveInput;
    Rigidbody2D rgb;
    Animator amt;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    float tronglucbandau;
    AudioSource AudioSource;


    bool Alive = true;

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        amt = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        tronglucbandau = rgb.gravityScale;
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        AudioSource = GetComponent<AudioSource>();
    }



    void Update()
    {
        if(!Alive){  return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }


    void OnFire(InputValue value)
    {
        if (!Alive) { return; }
        Instantiate(Bullet, Gun.position, transform.rotation);
    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }



    void Run()
    {
        if (!Alive) { return; }
        Vector2 playerVlocity = new Vector2(moveInput.x * Tocdochay, rgb.velocity.y); //Thay đổi x giữ nguyên y;
        rgb.velocity = playerVlocity;
        bool playerhasHorizontalSpeed = Mathf.Abs(rgb.velocity.x) > Mathf.Epsilon; //Nếu người chơi đang di chuyển thì thực hiện
        amt.SetBool("IsRunning", playerhasHorizontalSpeed);                      //Setbool đặt giá trị cho kiêu bool 
    }


    //Lật chiều hình ảnh nhân vật theo hương di chuyển
    void FlipSprite()
    {
        if (!Alive) { return; }
        bool playerhasHorizontalSpeed = Mathf.Abs(rgb.velocity.x) > Mathf.Epsilon; // Mathf.Epsilon là giá trị nhỏ nhất mà 1 số kiểu float khác 0(0.0000001,-0.0000001) gần với 0 nhất
                                                                                   // Mathf.abs trả về giá trị tuyệt đối của số đó kiểu float
        if(playerhasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rgb.velocity.x), 1f); //Mathf: trả về gt -1 nếu rgb.velocity.x âm , 0 = 0 , 1 = dương.
        }    

    }


    void OnJump(InputValue value)
    {
        if (!Alive) { return; }
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            rgb.velocity += new Vector2(0f, JumpSpeed );
        }    
    }


    void ClimbLadder()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rgb.gravityScale = tronglucbandau;
            amt.SetBool("Isclimb", false);
            return;
        }
        Vector2 ClimbVlocity = new Vector2(rgb.velocity.x, moveInput.y * tocdoleo);
        rgb.velocity = ClimbVlocity;
        bool playerClimbing = Mathf.Abs(rgb.velocity.y) > Mathf.Epsilon; //Nếu người chơi đang di chuyển thì thực hiện
        amt.SetBool("Isclimb", playerClimbing);
        rgb.gravityScale = 0;
    }


    void Die()
    {
        if(myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("enermy","Trap","Boss"))) // khi mà nhân vật chạm vào lớp ennermy thì sự sống bàng false 
        {
            Alive = false;
            amt.SetTrigger("Dying");
            rgb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

    }


    public void DieByBoos()
    {
            Alive = false;
            amt.SetTrigger("Dying");
            rgb.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}

