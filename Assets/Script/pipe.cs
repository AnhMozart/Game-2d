using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipe : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;

    private void Update()
    {
        transform.position += Vector3.left * movespeed * Time.deltaTime;
    }
}
