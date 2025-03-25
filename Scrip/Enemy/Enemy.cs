using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    public enum Stats
    {
        Idle, Running
    }

    [Header("Setting")]
    [SerializeField] private float SearchRadius;
    [SerializeField] private float MoveSpeed;
    private Stats stats;
    private Transform TagetTranform;

    private void Update()
    {
        ManagerStates();
    }


    private void ManagerStates()
    {
        switch (stats)
        {
            case Stats.Idle:
                SearchForTarget();
                break;


            case Stats.Running:
                MoveSpeedForTarget();
                break;

        }

    }   
    
    
    private void SearchForTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, SearchRadius);

        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent(out Runner runner))
            {
                if(runner.IsTarget())
                    continue;
                runner.SetTagert();
                TagetTranform = runner.transform;
                StartRunningTowardsTarget();
                break;

            }    
        }
    }

    private void StartRunningTowardsTarget()
    {
        stats = Stats.Running;
        GetComponent<Animator>().Play("Run");
    }


    private void MoveSpeedForTarget()
    {
        if (TagetTranform == null) return;

        transform.position = Vector3.MoveTowards(transform.position, TagetTranform.position, Time.deltaTime * MoveSpeed);

        if (Vector3.Distance(transform.position, TagetTranform.position) < 1f)
        {
            Destroy(TagetTranform.gameObject);
            Destroy(gameObject);

        }

    }
}
