using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour
{

    Animator animator;
    NavMeshAgent navmeshAgent;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (navmeshAgent.velocity.magnitude > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
            navmeshAgent.stoppingDistance = 1.5f;
        }
    }

    public void CuttingOneHand(bool animate)
    {
        Debug.Log("Cutting animation enabled");
        animator.SetBool("cutOneHand", animate);
    }

    public void StopAllAnimations()
    {
        animator.SetBool("cutOneHand", false);
    }

}
