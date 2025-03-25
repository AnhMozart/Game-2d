using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro bubbleText;
    [SerializeField] private Transform RunnerCount;

    private void Update()
    {
        bubbleText.text = RunnerCount.childCount.ToString();
        if(RunnerCount.childCount <= 0)
        {
            Destroy(gameObject);
        }    
    }
}
