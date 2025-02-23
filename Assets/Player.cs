using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float jump = 5f;
    //[SerializeField] private float horizontalForce = 1f;
    private Rigidbody2D rgb;
    private Animator arm;
    private SpriteRenderer sr;

    [Header("CheckGround")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;

    [Space]
    [Header("Fx")]
    [SerializeField] private Material hitMat;
    private Material originalMat;
    private Coroutine flashCoroutine; // Biến theo dõi coroutine

    private void Start()
    {
        rgb = this.GetComponent<Rigidbody2D>();
        arm = this.GetComponent<Animator>();
        sr  = this.GetComponent<SpriteRenderer>();
        originalMat = sr.material;
    }

    private void Update()
    {
        Jump();
        SetAnimation();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) & IsGround())
        {
            //rgb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);//nhảy lên
            //rgb.AddForce(Vector2.right * horizontalForce, ForceMode2D.Impulse); 
            Game_Sound.instans.PlayJumpSound();
            rgb.linearVelocity = Vector2.up * jump;
            //transform.position = new Vector2(0f, jump * transform.position.y *Time.deltaTime);
        }
    }


    private void SetAnimation()
    {
        if (!IsGround())
        {
            arm.SetBool("Jump",true);
        }    
        else
        {
            arm.SetBool("Jump", false);
        }    
    }    


    private bool IsGround() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, groundLayer);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheck.position, Vector2.down * groundCheckRadius);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("AddCoin"))
        {
            Debug.Log("Bạn đã được cộng điểm");
            GameManager.instance.AddCoin(100);
        }

        else if (collision.CompareTag("Obstacle"))
        {
            GameManager.instance.UppdatetoLive();
            if(flashCoroutine == null)
                StartCoroutine(FlashFX());
        }
    }

    private IEnumerator FlashFX() //Hàm chuyển màu khi bị đánh
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originalMat;
        flashCoroutine = null; // Gán lại null khi coroutine kết thúc
    }

}
