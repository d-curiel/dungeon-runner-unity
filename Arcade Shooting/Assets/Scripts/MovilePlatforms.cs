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
        targetPosition = transform.position;
    }
    void FixedUpdate()
    {
        if (System.Math.Round(gameObject.transform.position.z, 2)  == System.Math.Round(pointA.transform.position.z, 2) )
        {
            targetPosition = (pointB.transform.position - transform.position).normalized;
        }
        else if (System.Math.Round(gameObject.transform.position.z, 2) == System.Math.Round(pointB.transform.position.z, 2))
        {
            targetPosition = (pointA.transform.position - transform.position).normalized;
        }
        transform.Translate(targetPosition * 1.5f * Time.deltaTime);
    }
}
