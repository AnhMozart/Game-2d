using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Pipe : MonoBehaviour
{
    [SerializeField] private float maxtranform;
    [SerializeField] private float timespawn;
    [SerializeField] private GameObject PipePrefab;
    [SerializeField] private ObjectPool pipePool;
    [SerializeField] private float Timmer = 0.001f;
    
    private float deltalTimmer;


    private void Start()
    {
        SpawnPipe();
    }


    private void Update()
    {
        deltalTimmer += Time.deltaTime;
        if(deltalTimmer > timespawn)
        {
            SpawnPipe();
            deltalTimmer = 0;
        }

        Debug.Log(timespawn);
    }


    private void SpawnPipe()
    {
        GameObject pipe = pipePool.Getoject();

        if(pipe != null)
        {
            pipe.transform.position = transform.position + new Vector3(0,UnityEngine.Random.Range(-maxtranform, maxtranform));
            StartCoroutine(ReturnPool(pipe));
            UpdateTimer();
        } 
    }


    IEnumerator ReturnPool(GameObject _pipe)
    {
        yield return new WaitForSeconds(10f); // Thời gian tồn tại của ống
        UpdateTimeSpawn();
        if (_pipe != null) // Kiểm tra _pipe trước khi trả về pool
        {
            pipePool.ReturnObject(_pipe);
        }
    }


    private void UpdateTimer()
    {
        Timmer += 0.001f * Time.deltaTime;
    }    


    private float UpdateTimeSpawn()
    {
        timespawn -= Timmer;
        return Math.Max(timespawn, 1);
    }    
}
