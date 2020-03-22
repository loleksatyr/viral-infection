using UnityEngine;
using UnityEngine.AI;


public class Office_Script : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private GameObject position;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
  agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
         if (points.Length == 0)
            return;

         agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        
        Collider[] customerColliders = Physics.OverlapSphere(gameObject.transform.position, 1f);
        foreach (Collider collider in customerColliders)
        {
            if (collider.gameObject.tag == "Customer" && collider.gameObject.GetComponent<CustomerController>().hasVirus)
            {
                agent.destination = collider.gameObject.transform.position;
                Collider[] customerColliders2 = Physics.OverlapSphere(gameObject.transform.position, 0.1f);
                foreach (Collider colliders in customerColliders2)
                {
                    if (colliders.gameObject.tag == "Customer" && colliders.gameObject.GetComponent<CustomerController>().hasVirus)
                    {
                        Destroy(colliders.gameObject);
                    }
                }
            }
            else
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    GotoNextPoint();

                if (!agent.isOnNavMesh)
                    return;
            }
        }
        
      //  }
    }
}

