using UnityEngine;

public class Enemy_Group : MonoBehaviour
{
    [SerializeField] private Enemy enemyprefab;
    [SerializeField] private Transform Enemyparent;
    [SerializeField] private float angle; // góc giữa các kẻ địch
    [SerializeField] private float radius;


    private void Start()
    {
        CreateEnemy();
    }


    private void CreateEnemy()
    {
        int amount = Random.Range(5, 50);
        for(int i = 0; i < amount; i++)
        {

            Vector3 enemyLocalPosition = GetEnemyLocalPosition(i);

            Vector3 enemyWorldPosition = Enemyparent.TransformPoint(enemyLocalPosition); // chuyển tọa độ cục bộ sang tọa độ thế giới game

            Instantiate(enemyprefab, enemyWorldPosition, Quaternion.identity, Enemyparent);
        } 
            
    }    


    private Vector3 GetEnemyLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * angle * index);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * angle * index);
        return new Vector3(x, 0, z);
    }

}
