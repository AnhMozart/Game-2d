using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [Header("Move_Infor")]
    [SerializeField] protected float baseMoveSpeed = 1.5f; // Tốc độ cơ bản
    [SerializeField] private float moveSpeedVariation = 1.5f; // Biến thiên tốc độ
    [SerializeField] protected float moveSpeed; // Tốc độ di chuyển hiện tại



    protected Player player;

    [Header("Heath_Infor")]
    [SerializeField] protected float maxHP = 100f;
    [SerializeField] protected float currrentHP;
    [SerializeField] private Image HpBar;

    [Header("Damage_Infor")]
    [SerializeField] protected float damagebasic = 10f;
    [SerializeField] protected float damageStay = 1f;


    protected virtual void Start()
    {
        // Tính toán tốc độ di chuyển ngẫu nhiên
        moveSpeed = baseMoveSpeed + Random.Range(-moveSpeedVariation, moveSpeedVariation);

        player = FindAnyObjectByType<Player>();

        currrentHP = maxHP;
        UpdateHpBar();
    }


    protected virtual void Update()
    {
        MoveToPlayer();
    }


    protected void MoveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            Flip();
        }
    }


    protected void Flip()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x? -1:1,1,1);
        }
    }


    public virtual void TakeDamege(float _damage)
    {
        currrentHP -= _damage;
        UpdateHpBar();
        currrentHP = Mathf.Max(currrentHP, 0);
        if(currrentHP <= 0)
        {
            Die();
        }


    }


    protected virtual void Die()
    {
        Destroy(gameObject);
    }


    protected void UpdateHpBar()
    {
        if (HpBar != null)
        {
            HpBar.fillAmount = currrentHP / maxHP;
        }
    }
}
