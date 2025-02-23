using UnityEngine;

public class Bear_explosion : Enemy
{
    [SerializeField] private GameObject explosion;

    private void CreateExplosion()
    {
        if (explosion != null)
        {
            Instantiate(explosion,transform.position, Quaternion.identity);
        }
    }

    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            CreateExplosion();
        }
    }
}
