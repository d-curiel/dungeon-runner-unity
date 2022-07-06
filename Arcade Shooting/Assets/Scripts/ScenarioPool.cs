using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioPool : MonoBehaviour
{

    public static ScenarioPool scenarioPool;

    [SerializeField]
    List<GameObject> scenariosPrefab;
    [SerializeField]
    int scenarios;
    List<GameObject> pool;
    // Start is called before the first frame update

    private void Awake()
    {
        scenarioPool = this;
    }
    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < scenarios; i++)
        {
            GameObject tmp = Instantiate(scenariosPrefab[0]);
            tmp.SetActive(false);
            pool.Add(tmp);

            tmp = Instantiate(scenariosPrefab[1]);
            tmp.SetActive(false);
            pool.Add(tmp);


            tmp = Instantiate(scenariosPrefab[2]);
            tmp.SetActive(false);
            pool.Add(tmp);

        }

    }

    public GameObject GetScenario()
    {
        return pool.Find(scenario => !scenario.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
