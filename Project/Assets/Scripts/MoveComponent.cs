using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public Vector3 VectorMoveDirection;
    Rigidbody rb;
    public float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(VectorMoveDirection * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }
}
