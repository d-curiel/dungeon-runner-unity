using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilePlatforms : MonoBehaviour
{

    [SerializeField]
    GameObject pointA;

    [SerializeField]
    GameObject pointB;

    Vector3 targetPosition;
    private void Start()
    {

        targetPosition = (pointB.transform.position - transform.position).normalized;
    }
    void FixedUpdate()
    {
        if(Vector3.Distance(pointA.transform.position, gameObject.transform.position) < 0.2f)
        {
            targetPosition = (pointB.transform.position - transform.position).normalized;

        }
        else if (Vector3.Distance(pointB.transform.position, gameObject.transform.position) < 0.2f)
        {

            targetPosition = (pointA.transform.position - transform.position).normalized;
        }

        transform.Translate(targetPosition * 5f * Time.deltaTime);
    }
}
