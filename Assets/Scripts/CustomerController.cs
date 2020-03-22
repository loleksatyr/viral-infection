using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private float sphereRadius = 15f;
    public bool isActive = false;
    public GameObject nextIndicator;
    public GameObject currentIndicator;
    public GameObject camera;
    public GameObject virusParticlePrefab;
    private GameObject virusParticle;
    public bool hasVirus = false;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private float progress = 0f;
    public GameplayController gameplayController;

    private void Start()
    {
        InvokeRepeating("IncreaseProgress", 1f, 1f);
        gameplayController = camera.GetComponent<GameplayController>();
    }

    void IncreaseProgress()
    {
        if (isActive && hasVirus == false)
        {
            progress++;
        }

        if (progress > 5 && hasVirus == false)
        {
            hasVirus = true;
            virusParticle = Instantiate(virusParticlePrefab);
            gameplayController.AddScore();
        }
    }

    void Activate()
    {
        isActive = true;
    }

    IEnumerator SwitchCustomer(GameObject customer)
    {
        yield return new WaitForSeconds(1/20);
        isActive = false;
        customer.GetComponent<CustomerController>().isActive = true;
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            currentIndicator.transform.position = gameObject.transform.position + new Vector3(0, 2f, 0);
            camera.transform.position = gameObject.transform.position + cameraOffset;

            float nearestDistance = float.MaxValue;
            GameObject nearestCustomer = null;
            nextIndicator.transform.position = new Vector3(-10000f, -10000f, -10000f);
            Collider[] customerColliders = Physics.OverlapSphere(gameObject.transform.position, sphereRadius);

            foreach (Collider collider in customerColliders)
            {
                if (collider.gameObject.tag == "Customer" && collider.gameObject.GetComponent<CustomerController>().isActive == false)
                {
                    float distance = Vector3.Distance(gameObject.transform.position, collider.gameObject.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestCustomer = collider.gameObject;
                    }
                }
            }

            if (nearestCustomer != null)
            {
                nextIndicator.transform.position = nearestCustomer.transform.position + new Vector3(0, 2f, 0);
            }

            if (Input.GetKeyDown("space") && nearestCustomer != null)
            {
                StartCoroutine(SwitchCustomer(nearestCustomer));
            }
        }

        if (hasVirus)
        {
            if (gameObject != null && virusParticle != null)
            {
                virusParticle.transform.position = gameObject.transform.position;
            }
        }
    }
}
