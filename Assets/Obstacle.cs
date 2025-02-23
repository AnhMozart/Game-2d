using UnityEngine;

public class Obstacle : MonoBehaviour
{



    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.position += Vector3.left * GameManager.instance.GetGameSpeed() * Time.deltaTime;
    }

}
