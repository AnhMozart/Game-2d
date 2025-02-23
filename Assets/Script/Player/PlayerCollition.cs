using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollition : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletBoss"))
        {
            Player player = GetComponent<Player>();
            player.TakeDamage(30);
        }
        else if(collision.CompareTag("Milk"))
        {
            Debug.Log("Ban da lay lai duoc sua");
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Energy"))
        {
            GameManager.instance.AddEnergy();
            AudioManager.instance.PlayEnergySound();
            Destroy(collision.gameObject);
        }    
    }

 
}
