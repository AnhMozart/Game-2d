using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] protected float damagePlayer = 1f;

    public Rigidbody2D rgb;

    protected virtual void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }    


    protected virtual void Update()
    {

    }    


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
    }

}
