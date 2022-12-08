using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

    [SerializeField, Range(0.0f, 360f)]
    private float angle = 90.0f;

    [SerializeField, Range(0.0f, 5.0f)]
    private float speed = 2.0f;

    [SerializeField, Range(0.0f, 10.0f)]
    private float startTime = 2.0f;

    [SerializeField]
    private Vector3 myVector;

    Quaternion start, end;

    private void Update()
    {
        start = PendulumRotation(angle);
        end = PendulumRotation(-angle);

        startTime += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(start, end, (Mathf.Sin(startTime * speed + Mathf.PI / 2) + 1.0f) / 2.0f);
        Debug.Log((Mathf.Sin(startTime * speed + Mathf.PI / 2) + 1.0f) / 2.0f);
        
    }
    private void Start()
    {
      
    }
    private void ResetTimer()
    {
        startTime = 0.0f;
    }

    Quaternion PendulumRotation(float angle)
    {
        var pendulumRotation = Quaternion.Euler(Vector3.zero);
        var angleZ = pendulumRotation.eulerAngles.z + angle;
        if (angleZ > 180)
            angleZ -= 360;
        else if (angleZ < -180)
            angleZ += 360;
        pendulumRotation.eulerAngles = Vector3.Scale(new Vector3(angleZ, angleZ, angleZ), myVector);
        return pendulumRotation;
    }
}
