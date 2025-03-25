using UnityEngine;

public class Runner : MonoBehaviour
{
    public bool isTarget;

    public void SetTagert()
    {
        isTarget = true;
    }

    public bool IsTarget()
    { 
        return isTarget; 
    }
}
