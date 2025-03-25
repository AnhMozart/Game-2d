using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [SerializeField] private Transform RunnerParent;
    [SerializeField] private GameObject RunnerPrefab;
    [SerializeField] private PlayerAnimator PlayerAnimator;


    [Header("Setting")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;


    void Start()
    {
        
    }


    private void Update()
    {
        if(!GameManager.instance.IsGameState()) { return; }

        PlaceRunners();
        if (RunnerParent.childCount <= 0)
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
    }


    private void PlaceRunners()
    {
        for (int i = 0; i < RunnerParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            RunnerParent.GetChild(i).localPosition = childLocalPosition;
            PlayerAnimator.Run();
        }
    }   
    

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * angle * index);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * angle * index);
        return new Vector3(x,0,z);
    }    


    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(RunnerParent.childCount);
    }    


    public void ApplyBonus(BonusType _bonusType, int _bonusAmount)
    {
        switch(_bonusType)
        {
            case BonusType.Addition:
                AddRunner(_bonusAmount);
                break;

            case BonusType.Multiply:
                int MultipleRunner = (RunnerParent.childCount * _bonusAmount) - RunnerParent.childCount;
                AddRunner(MultipleRunner);
                break;

            case BonusType.Differrence:
                RemoveRunner(_bonusAmount);
                break;

            case BonusType.Division:
                int DivitionRunner = RunnerParent.childCount - (RunnerParent.childCount / _bonusAmount);
                RemoveRunner(DivitionRunner);
                break;
        }    
    }    


    private void AddRunner(int index)
    {
        for (int i = 0; i < index; i++)
        {
                Instantiate(RunnerPrefab, RunnerParent);
        }
    } 


    private void RemoveRunner(int index)
    {
        if(index > RunnerParent.childCount)
            index = RunnerParent.childCount;

        int RunnerAmount = RunnerParent.childCount;
        for(int i = RunnerAmount - 1; i >= RunnerAmount - index; i--)
        {
            Transform runnerDestroy = RunnerParent.GetChild(i);
            runnerDestroy.SetParent(null);
            Destroy(runnerDestroy.gameObject);
        }    
    } 
        
        
}
