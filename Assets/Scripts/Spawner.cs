using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float SpawnInterval = 5f;
    public List<GameObject> CustomerPrefabs;

    void Start()
    {
        InvokeRepeating("SpawnCustomer", 1f, 1f);
    }

    void SpawnCustomer()
    {
        if (CustomerPrefabs.Count > 0) {
            Random random = new Random();
            Instantiate(CustomerPrefabs[(int)Mathf.Round(Random.Range(0, CustomerPrefabs.Count))], gameObject.transform.position, Quaternion.identity);
        }
    }
}
