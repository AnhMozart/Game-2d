using UnityEngine;

public class Collapsing_Floor : Trap
{
    private Vector3 CurrenPostion;
    protected override void Start()
    {
        base.Start();
        CurrenPostion = transform.position;
    }


    protected override void Update()
    {
        base.Update();
    }



    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke("Set_Falling_Platform", 1f);
        }    
    }


    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        Invoke("Set_Falling_Platform_1", 3f);
    }



    private void Set_Falling_Platform()
    {
        rgb.bodyType = RigidbodyType2D.Dynamic;
    }


    private void Set_Falling_Platform_1()
    {
        rgb.bodyType = RigidbodyType2D.Static;
        transform.position = CurrenPostion;
    }

}
