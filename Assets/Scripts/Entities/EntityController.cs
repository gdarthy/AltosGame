using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EntityController : MonoBehaviour
{

    //Waypoint system
    UnityEngine.AI.NavMeshAgent nma;
    RaycastHit rHit;
    Vector3 origin;
    Vector2 offset;
    Vector3[] waypoints;

    public float waypointRadius = 5;
    public int waypointCount = 5;
    int waypointIndex;
    float distance;
    bool patrolPending;

    //Player interaction
    bool movingTowardsPlayer = false;
    Transform player;
    bool inRange;
    bool playerReached;
    bool playerInteraction;
    float playerDistance;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Waypoint system
        waypoints = new Vector3[waypointCount];
        nma = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nma.stoppingDistance = 0.1f;
        nma.angularSpeed = 500;

        origin = transform.position;
        origin.y += 10;

        GenerateWaypoints();

        waypointIndex = 0;
        playerInteraction = false;
        EntityToPlayerDistance();
        StartPatrol(waypoints[waypointIndex]);

    }

    //Waypoint System (WS)

    void GenerateWaypoints()
    {
        for (int c = 0; c < waypointCount; c++)
        {
            offset = new Vector2(Random.Range(-waypointRadius, waypointRadius) + origin.x, Random.Range(-waypointRadius, waypointRadius) + origin.z);
            if (Physics.Raycast(new Vector3(offset.x, origin.y, offset.y), -Vector3.up, out rHit))
            {
                waypoints[c] = rHit.point;
            }
        }
    }

    void StartPatrol(Vector3 destination)
    {
        patrolPending = true;
        nma.SetDestination(destination);
        nma.isStopped = false;


        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void ResumePatrol()
    {

        movingTowardsPlayer = false;
        playerInteraction = false;
        nma.stoppingDistance = 0.1f;
        EntityToWaypointDistance();
        StartPatrol(waypoints[waypointIndex]);


        gameObject.GetComponent<Renderer>().material.color = Color.cyan;
    }

    void SetRandomWaypointIndex()
    {
        int oldIndex = waypointIndex;
        waypointIndex = Random.Range(0, waypointCount);
        if (waypointIndex == oldIndex)
        {
            SetRandomWaypointIndex();
        }
    }

    void EntityToWaypointDistance()
    {
        distance = Vector3.Distance(transform.position, waypoints[waypointIndex]) - (nma.baseOffset * 1.85f);
    }

    public bool IsPatrolPending()
    {
        return patrolPending;
    }

    //WS

    //PlayerInteraction (PI)

    public void MoveTowardsPlayer()
    {
        nma.stoppingDistance = 2f;
        nma.SetDestination(player.position);
        nma.isStopped = false;
        movingTowardsPlayer = true;
        playerReached = false;
        playerInteraction = true;


        gameObject.GetComponent<Renderer>().material.color = Color.red;

    }

    public void StopPatrol()
    {
        patrolPending = false;
        nma.isStopped = true;


        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    void EntityToPlayerDistance()
    {
        playerDistance = Vector3.Distance(transform.position, player.position) - (nma.baseOffset * 1.85f);
    }

    // If player is inside entity radius
    public bool IsPlayerInRange(float radius)
    {
        inRange = playerDistance < radius ? true : false;
        return inRange;
    }

    // If entity reached player
    public bool IsPlayerReached()
    {
        return playerReached;
    }

    //PI

    void Update()
    {
        //Player interaction
        if (movingTowardsPlayer)
        {
            EntityToPlayerDistance();
            if (playerDistance <= 1f)
            {
                movingTowardsPlayer = false;
                playerReached = true;
                nma.isStopped = true;
            }
        }
        else
        {
            playerReached = false;
            EntityToPlayerDistance();
        }


        //Waypoint System
        if (distance <= 0.1f && !movingTowardsPlayer)
        {
            nma.isStopped = true;
            SetRandomWaypointIndex();
            EntityToWaypointDistance();
            StartPatrol(waypoints[waypointIndex]);
        }
        else if (!playerInteraction)
        {
            EntityToWaypointDistance();
        }

    }
}
