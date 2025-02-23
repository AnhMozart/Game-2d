using UnityEngine;

public class Bullet_Boss : MonoBehaviour
{
    private Vector3 direction;

    private void Start()
    {
        Destroy(gameObject,5f);
    }

    private void Update()
    {
        if(direction != Vector3.zero)
        {
            transform.position += direction * Time.deltaTime;
        }

    }

    public void SetDirection(Vector3 _directionplayer)
    {
        direction = _directionplayer;
    }

}
