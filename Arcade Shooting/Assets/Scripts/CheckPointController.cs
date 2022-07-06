using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [SerializeField]
    GameObject spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ScenarioSpawner._instance.Spawn(spawnPoint.transform.position);
        }
        
    }
}
