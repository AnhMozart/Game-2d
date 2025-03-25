using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private Vector3 size;



    public float GetLength()
    {
        return size.z;
    }    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }


    public Vector3 GetSize()
    {
        return size;
    }


}
