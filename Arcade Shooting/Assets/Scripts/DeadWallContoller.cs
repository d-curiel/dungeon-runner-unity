using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadWallContoller : MonoBehaviour
{   [SerializeField]
    float approachSpeed = 10f;
    [SerializeField]
    float speedIncrementTime = 15f;
    [SerializeField]
    float incrementSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = transform.position + (Vector3.right * approachSpeed * Time.deltaTime);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * approachSpeed * 2f);

        if(Time.deltaTime % speedIncrementTime == 0)
        {
            approachSpeed += incrementSpeed;
        }
    }

}
