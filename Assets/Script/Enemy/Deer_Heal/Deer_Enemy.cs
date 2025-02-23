using UnityEngine;

public class Deer_Enemy : Enemy
{
    [SerializeField] private float Heal = 20f; //Lượng máu hồi là 20
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damagebasic);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.TakeDamage(damageStay); 
        }
    }

    protected override void Die()
    {
        HealPlayer();
        base.Die();
    }

    private void HealPlayer()
    {
        if(player != null)
        {
            player.HealthUpdate(Heal);
        }    
            
    }

}
