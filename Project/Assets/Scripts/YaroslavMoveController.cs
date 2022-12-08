using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YaroslavMoveController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private Transform _cameraCharacter;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeResetFreeRotation;

    public bool canMove = true;
    public bool isRuning;
    public bool inGround;
    public bool contactWithMechanism = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {       
            StandUp();
        }

        Jump();
        if (GetVectorMove() == Vector3.zero) return;

        RotationCharacter();
        Move();

    }

    private void Move()
    {
        isRuning = true;

        _rigidbody.MovePosition(transform.position + GetVectorMove() * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (inGround && Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(GetVectorMove() + transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private Vector3 GetVectorMove()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 cameraCharacter = Vector3.Scale(_cameraCharacter.forward, new Vector3(1, 0, 1));
        Vector3 mainCameraRight = Vector3.Scale(_cameraCharacter.right, new Vector3(1, 0, 1));

        Vector3 movementInCameraForwardDirection = cameraCharacter * verticalAxis;
        Vector3 movementInCameraRightDirection = mainCameraRight * horizontalAxis;

        Vector3 movementForward = movementInCameraForwardDirection + movementInCameraRightDirection;

        return new Vector3(movementForward.x, 0f, movementForward.z).normalized;
    }

    private void RotationCharacter()
    {
        float targetAngle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + _cameraCharacter.transform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);
    }

    private void CheckStayGround(Transform ground)
    {
        inGround = ground != null && ground.CompareTag("Ground") ? true : false;
    }

    private void CheckContactWithMechanism(Transform contactObject)
    {
        contactWithMechanism = contactObject != null && contactObject.CompareTag("Mechanism") ? true : false;
    }

    private void OnCollisionStay(Collision collision)
    {
        CheckStayGround(collision.transform);
        //CheckContactWithMechanism(collision.transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        CheckStayGround(null);
        //CheckContactWithMechanism(null);
    }

    private void StandUp()
    {
        for (int i = 0; i < 5000; i++)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), 0.05f * Time.deltaTime);
            if (transform.rotation == Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z)) return;
        } 
    }

    private void ChangeFreezeRotationPlayer(bool state)
    {
        _rigidbody.freezeRotation = !state;
    }

    private IEnumerator ResetFreezeRotationPlayer()
    {
        yield return new WaitForSeconds(timeResetFreeRotation);

        if (inGround && !contactWithMechanism)
        {
            ChangeFreezeRotationPlayer(false);
        }
    }

}

