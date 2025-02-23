using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public Joystick Joystick;

    private Rigidbody2D rgb;
    private SpriteRenderer srt;
    private Animator arm;

    [Header("Heath_Infor")]
    [SerializeField] private float maxhp = 100f;
    [SerializeField] private float currentHP;
    [SerializeField] private Image HPbar;


    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        srt = GetComponent<SpriteRenderer>();
        arm = GetComponent<Animator>();
    }


    private void Start()
    {
        currentHP = maxhp;
        UpdateHPBar();
    }


    private void Update()
    {
        Moving();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.PauseMenu();
        }
    }


    private void Moving()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // di chuyển bằng phím
        //Vector2 playerInput = new Vector2(Joystick.Horizontal, Joystick.Vertical);

        rgb.linearVelocity = playerInput.normalized * moveSpeed;

        FlipPlayer(playerInput);

        if(playerInput != Vector2.zero)
            arm.SetBool("IsRun",true);

        else
            arm.SetBool("IsRun", false);

    }


    private void FlipPlayer(Vector2 playerInput)
    {
        if (playerInput.x < 0)
        {
            srt.flipX = true;
        }
        else if (playerInput.x > 0)
        {
            srt.flipX = false;
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHP -= _damage;
        UpdateHPBar();
        currentHP = Mathf.Max(currentHP, 0);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.instance.GameOver();
        Destroy(gameObject);
    }

    private void UpdateHPBar()
    {
        HPbar.fillAmount = currentHP/maxhp;
    }


    public void HealthUpdate(float _Heal)
    {
        if(currentHP < maxhp)
        {
            currentHP += _Heal;
        }    
        currentHP = Mathf.Min(currentHP, maxhp);
        UpdateHPBar();
    }    
}
