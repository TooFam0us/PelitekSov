using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScripts : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    public Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (agent.remainingDistance > 0.1f)
        {
            animator.SetBool("IsMoving", true);
        }
        if (agent.remainingDistance < 0.1f)
        {
            animator.SetBool("IsMoving", false);
        }
    }
}

