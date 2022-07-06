using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioSpawner : MonoBehaviour
{

    public static ScenarioSpawner _instance;
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
    }

    public void Spawn(Vector3 nextPosition)
    {
        GameObject nextScenario = ScenarioPool.scenarioPool.GetScenario();
        nextScenario.transform.position = nextPosition;
        nextScenario.transform.rotation = Quaternion.identity;
        nextScenario.SetActive(true);
    }

    public void DeleteScenario(GameObject scenario)
    {
        StartCoroutine(HiddeScenario(scenario));
    }

    IEnumerator HiddeScenario(GameObject scenario)
    {
        yield return new WaitForSeconds(2.0f);

        scenario.SetActive(false);

    }
}
