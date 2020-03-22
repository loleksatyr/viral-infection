using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private float sphereRadius = 15f;
    public bool isActive = false;
    public GameObject nextIndicator;
    public GameObject currentIndicator;

    void FixedUpdate()
    {
        if (isActive)
        {
            currentIndicator.transform.position = gameObject.transform.position + new Vector3(0, 2f, 0);

            nextIndicator.transform.position = new Vector3(-10000f, -10000f, -10000f);
            Collider[] customerColliders = Physics.OverlapSphere(gameObject.transform.position, sphereRadius);
            foreach (Collider collider in customerColliders)
            {
                Debug.Log(collider.gameObject.tag);

                if (collider.gameObject.tag == "Customer" && collider.gameObject.GetComponent<CustomerController>().isActive == false)
                {
                    nextIndicator.transform.position = collider.gameObject.transform.position + new Vector3(0, 2f, 0);
                    break; 
                }
            }
        }
    }
}
