using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class ListofProducts : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] products;
    public GameObject checkout;
    public float rand;
    public bool firstProduct = false;
    public float firstRand = Random.RandomRange(0f, 10f);
    int product = 0;
    public bool endShop = false;



    private void FixedUpdate()
    {
        int firstRandint = (int)firstRand;

        agent.SetDestination(products[firstRandint].transform.position);

        if (firstProduct)
        {
            int nextRandint = (int)rand;
            agent.SetDestination(products[nextRandint].transform.position);
        }
        if (endShop)
        {
            agent.SetDestination(checkout.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        firstProduct = true;
        rand = Random.RandomRange(0f, 10f);
        product++;
        if (product == 5)
            endShop = true;
    }
}
