using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] transforms;
    [SerializeField] protected float SpawnerTimer = 5f; //Thời gian ban đầu
    [SerializeField] protected float MinSpanwTimer = 2f; //Thời gian tối thiểu

    [SerializeField] private float CurrentSpawnTimer;
    [SerializeField] protected float speedTimer = 0.01f; //Tốc độ tăng thời gian 


    private void Start()
    {
        StartCoroutine(EnemySpawn());
        CurrentSpawnTimer = SpawnerTimer;
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(UpdateCurrenTimer());
            Debug.Log(UpdateCurrenTimer());
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Transform spawnPoint = transforms[Random.Range(0, transforms.Length)];

            Instantiate(enemy, spawnPoint.position,Quaternion.identity);

            UpdateTimer();
        }

    }


    private void UpdateTimer()
    {
        /*if (CurrentSpawnTimer <= MinSpanwTimer) Cách không cần dùng max mà trả luôn về giá trị CurrentSpawnTimer
        {
            speedTimer = 0;
        }*/

        speedTimer += 0.001f * Time.deltaTime;
    }


    private float UpdateCurrenTimer()
    {
        CurrentSpawnTimer -= speedTimer;
        return Mathf.Max(CurrentSpawnTimer, MinSpanwTimer); // tránh cho việc timer quá nhỏ
    }    
}
