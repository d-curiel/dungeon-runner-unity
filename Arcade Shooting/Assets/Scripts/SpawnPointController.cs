using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{

    [SerializeField]
    GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "DeadWall")
        {
            ScenarioSpawner._instance.DeleteScenario(parent);
        }
    }

}
