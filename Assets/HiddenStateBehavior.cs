using UnityEngine;

public class HiddenStateBehavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (MeshRenderer mesh in animator.transform.parent.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = false;
        }
        foreach (Canvas canvas in animator.transform.parent.GetComponentsInChildren<Canvas>())
        {
            canvas.enabled = false;
        }
        animator.SetBool("isClosing", false);
        animator.SetBool("isOpening", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (MeshRenderer mesh in animator.transform.parent.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = true;
        }
        foreach (Canvas canvas in animator.transform.parent.GetComponentsInChildren<Canvas>())
        {
            canvas.enabled = true;
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
