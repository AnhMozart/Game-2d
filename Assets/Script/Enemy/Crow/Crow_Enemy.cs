using Unity.VisualScripting;
using UnityEngine;

public class Crow_Enemy : Enemy
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.TakeDamage(damagebasic);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damageStay);
        }
    }
}
