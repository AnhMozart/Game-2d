using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform runnerParent;

    public void Run()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator arm = runner.GetComponent<Animator>();
            arm.Play("Run");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        {
            Transform runner = runnerParent.GetChild(i);
            Animator arm = runner.GetComponent<Animator>();
            arm.Play("Idle");
        }
    }
}
