using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float resetDistance = 8f; // Khoảng cách di chuyển trước khi reset

    private Vector2 defaulPosition;

    private void Start()
    {
        defaulPosition = transform.position;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if(transform.position.x < defaulPosition.x - resetDistance)
        {
            transform.position = defaulPosition;
        }    
    }
}
