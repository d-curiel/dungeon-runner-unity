using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    private void OnEnable()
    {
        GetChildObject(gameObject.transform, "Collectable");
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(_tag))
            {
                child.gameObject.SetActive(true);
            }else if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }
}
