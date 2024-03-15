using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolController : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> patrolPoints;
    public float waitTime = 2f;
    public bool pickRandom;

    private int currentPatrolIndex;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (pickRandom)
        {
            currentPatrolIndex = Random.Range(0, patrolPoints.Count);
        }
        else
        {
            currentPatrolIndex = 0;
        }
        StartCoroutine(PatrolSequence());
    }

    IEnumerator PatrolSequence()
    {
        while(enabled)
        {
            MoveToNextPatrolPoint();
            yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance);
            yield return new WaitForSeconds(waitTime);
        }
        yield break;
    }

    private void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Count == 0)
        {
            return;
        }
        agent.destination = patrolPoints[currentPatrolIndex].position; 
        if(pickRandom)
        {
            currentPatrolIndex = Random.Range(0, patrolPoints.Count);
        }
        else
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
    }
}