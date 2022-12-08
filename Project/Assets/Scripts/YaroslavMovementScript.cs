using UnityEngine;

public class YaroslavMovementScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody rigidbodyPlayer;
    [SerializeField] private Camera cameraCharacter;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool onGround;
    [SerializeField] private bool isDoubleJumped;
    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Jump();
    }
    void FixedUpdate()
    {
        if (GetDirectionVector() == Vector3.zero)
            return;

        RotationCharacter();
        Move();
    }

    private void RotationCharacter()
    {
        float targetAngle = Mathf.Atan2(GetDirectionVector().x, GetDirectionVector().z) * Mathf.Rad2Deg + cameraCharacter.transform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);
    }

    private Vector3 GetDirectionVector()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        return new Vector3(x, 0.0f, z);
    }

    private void Move()
    {
        rigidbodyPlayer.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        /* Unmerged change from project 'Assembly-CSharp.Player'
        Before:
                {

                    if (onGround)
        After:
                {

                    if (onGround)
        */
        {

            if (onGround)
            {
                rigidbodyPlayer.AddForce(GetDirectionVector() + transform.up * jumpForce, ForceMode.Impulse);
            }
            else if (!isDoubleJumped)
            {
                rigidbodyPlayer.AddForce(GetDirectionVector() + transform.up * jumpForce, ForceMode.Impulse);
                isDoubleJumped = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            onGround = true;
            isDoubleJumped = false;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        onGround = false;

    }
}
