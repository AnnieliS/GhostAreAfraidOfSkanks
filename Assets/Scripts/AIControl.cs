using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{

    [SerializeField] private float detectionRadius;
    [SerializeField] private float fleeRadius;

    private GameObject[] goalLocations;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isThink = false;
    #region animation parameters
    private string fly = "isFlying";
    private string idle = "isIdle";
    private string dance = "isDancing";
    private string flyOffset = "flyOffset";
    private string speedMulti = "speedMultiplier";
    private int danceCheck = 0;
    #endregion
    private float speedBase;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("Waypoint");
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger(fly);
        speedBase = agent.speed;
        anim.SetFloat(flyOffset, Random.Range(0.0f, 1.0f));
    }

    private void Update()
    {
        if (agent.remainingDistance < 1.0f && !isThink)
        {
            anim.SetTrigger(idle);
            Invoke("NewDestAfterIdle", Random.Range(4f, 8f));
            isThink = true;
        }
    }

    private void NewDestAfterIdle()
    {
        SetNewDest();
        AgentSpeedControl();
        CheckDance();
        isThink = false;
    }

    private void SetNewDest()
    {
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);

    }

    private void AgentSpeedControl()
    {
        float speedChange = Random.Range(-1f, 2f);
        float newSpeed = speedBase + speedChange;
        agent.speed = newSpeed;
        anim.SetFloat(speedMulti, speedChange / speedBase);
    }

    private void CheckDance()
    {
        danceCheck = Mathf.RoundToInt(Random.Range(0.0f, 10.0f));
        if (danceCheck > 5)
        {
            anim.SetTrigger(dance);
        }
        else
        {
            anim.SetTrigger(fly);
        }
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (transform.position - position).normalized;
            Vector3 newgoal = transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newgoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                agent.speed = speedBase + 15f;
                agent.angularSpeed = 500;
            }
        }
    }
}
