using Unity.VisualScripting;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [SerializeField] private CreateLevel[] level;
    private GameObject Finish;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
        else
        {
            Destroy(instance);
        }    
    }


    private void Start()
    {
        CreateLevelGame();
        Finish = GameObject.FindWithTag("Finish");

    }


    private void CreateLevelGame()
    {
        int currentLevel = GetLevel();
        currentLevel = currentLevel % level.Length;
        CreateLevel Level = level[currentLevel];
        CreateLevel(Level.chunks);
        
    }    


    private void CreateLevel(Chunk[] levelPrefab)
    {
        Vector3 ChunkPosition = Vector3.zero;
        for (int i = 0; i < levelPrefab.Length; i++)
        {
            Chunk createChunk = levelPrefab[i];
            if (i > 0)
            {
                ChunkPosition.z += createChunk.GetLength() / 2;
            }
            Chunk chunkInstance = Instantiate(createChunk, ChunkPosition, Quaternion.identity, transform);

            ChunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }


    public float ReturnPositionZFinish()
    {
        return Finish.transform.position.z;
    }    


    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Level", 0);
    }    

    public void FinishDisable()
    {
        Finish.GetComponent<BoxCollider>().enabled = false;
    }    
}
