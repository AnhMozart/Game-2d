using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] private float Damage_explosion = 50f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();

        if(collision.CompareTag("Player"))
        {
            player.TakeDamage(Damage_explosion);
        }


        if (collision.CompareTag("Enemy"))
        {
           enemy.TakeDamege(Damage_explosion);
        }

    }

    public void Destroy_Explosiion()
    {
        Destroy(gameObject);
    }

}
