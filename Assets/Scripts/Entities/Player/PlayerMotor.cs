using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/* This component moves our player.
		- If we have a focus move towards that.
		- If we don't move to the desired point.
*/

public delegate void DestinationReached();

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour
{


    NavMeshAgent agent;     // Reference to our NavMeshAgent
    bool pending;
    public event DestinationReached Reached;
    Vector3 target;

    bool isInteractable = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToObject(Vector3 position, bool isInteractable)
    {
        GetComponent<CharacterAnimation>().StopAllAnimations();
        pending = true;
        agent.SetDestination(position);
        this.isInteractable = isInteractable;
        if (isInteractable)
        {
            isInteractable = true;
            target = position;
        }
    }

    void Update()
    {

        if (pending && agent.remainingDistance <= agent.stoppingDistance && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Reached != null) Reached();
            pending = false;
        }
        if (isInteractable)
        {
            RotateTowards(target);
        }

    }

    void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}
