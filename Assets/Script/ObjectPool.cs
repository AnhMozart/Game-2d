using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prafab;
    [SerializeField] private int poolSize = 10;

    [SerializeField] private Queue<GameObject> pool; //HÀNG CHỜ


    private void Awake()
    {
        pool = new Queue<GameObject>();

        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(_prafab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }    
            
    }


    public GameObject Getoject()
    {
        if(pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }    
        else
        {
            GameObject obj = Instantiate(_prafab);
            return obj;
        }    
    } 


    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }    
        
}
