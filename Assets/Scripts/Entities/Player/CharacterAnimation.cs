using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void StoppingAllAnimations();
public class CharacterAnimation : MonoBehaviour
{

    Animator animator;
    NavMeshAgent navmeshAgent;

    public event StoppingAllAnimations Stopped;

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

    public void CuttingRightHand(bool animate)
    {
        animator.SetBool("cutRightHand", animate);
    }

    public void CuttingLeftHand(bool animate)
    {
        animator.SetBool("cutLeftHand", animate);
    }

    public void StopAllAnimations()
    {
        //Debug.Log("Stop all animations");
        if (Stopped != null) Stopped();
        //Debug.Log("Again");
        animator.SetBool("cutRightHand", false);
        animator.SetBool("cutLeftHand", false);
    }

}
