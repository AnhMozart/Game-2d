using UnityEngine;

public class Chicken_energy : Enemy
{
    [SerializeField] private GameObject energy;

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


    protected override void Die()
    {
        base.Die();

        GameObject Chicken_energy = Instantiate(energy,transform.position, Quaternion.identity);
        Destroy(Chicken_energy,5f);
    }
}
