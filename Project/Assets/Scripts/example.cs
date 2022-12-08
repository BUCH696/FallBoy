using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example : MonoBehaviour
{
    public Transform target;
    public float speed;
    void Update()
    {

        Vector3 targetDir = target.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);



        if (angle < -5.0F)
            print("turn left" + angle);
        else if (angle > 5.0F)
            print("turn right" + angle);
        else
            print("forward");
    }
}
