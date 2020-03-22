using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CustomerMovement : MonoBehaviour
{
    public List<Transform> availablePoints;
    [SerializeField] private List<Transform> points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GameObject position;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        if (availablePoints.Count > 0)
        {
            int pointsToWalk = (int)Mathf.Round(Random.Range(3, 10));
            Debug.Log(pointsToWalk);

            for (int i = 0; i < pointsToWalk; i++)
            {
                points.Add(availablePoints[(int)Mathf.Round(Random.Range(0, availablePoints.Count))]);
            }
        }

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        if (points.Count == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Count;
    }


    void FixedUpdate()
    {

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if (!agent.isOnNavMesh)
            return;
    }
}

