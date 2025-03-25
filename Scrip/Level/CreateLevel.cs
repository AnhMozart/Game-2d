using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName ="Scriptable Objects / Level", order = 0)]
public class CreateLevel : ScriptableObject 
{
    public Chunk[] chunks;
}
