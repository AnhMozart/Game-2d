using UnityEngine;

public class Obstacle_Spam : MonoBehaviour
{
    [SerializeField] private GameObject[] Obstacle;
    [SerializeField] private float SpamCoodown = 2f;
     private float nexttimer= 0f;

    private void Update()
    {

        if(nexttimer < Time.time)
        {
            nexttimer = Time.time + SpamCoodown;
            SpamnerObstacle();
        }    
    }


    private void SpamnerObstacle()
    {
        int randomOb = Random.Range(0, Obstacle.Length);
        GameObject obj = Instantiate(Obstacle[randomOb],transform.position,Quaternion.identity);
   
    }    
}
