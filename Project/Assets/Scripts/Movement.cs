using UnityEngine;

public class Movement : MonoBehaviour
{

    private CharacterController characterController;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    [SerializeField] private float movementSpeed = 6.0f;
    [SerializeField] private Camera cameraMain;

    Vector3 moveDirection;

    #region UnityMethods

    public void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraMain.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            Move();
        }


    }
    #endregion

    #region CustomMethods

    public void Move()
    {
        characterController.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
    }

    //public Vector3 GetMovementVector()
    //{
    //    var horizontal = Input.GetAxis("Horizontal");
    //    var vertical = Input.GetAxis("Vertical");
    //    return new Vector3(horizontal, moveDirection.y, vertical).normalized;
    //}

    //private float GetTargetAngle()
    //{
    //    return Mathf.Atan2(GetMovementVector().x, GetMovementVector().z) * Mathf.Rad2Deg + cameraMain.transform.eulerAngles.y;
    //}

    //private Vector3 GetMoveDir()
    //{
    //  return Quaternion.Euler(0, GetTargetAngle(), 0) * Vector3.forward;
    //}

    //private void RotateCharacter()
    //{
    //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, GetTargetAngle(), ref turnSmoothVelocity, turnSmoothTime);
    //    transform.rotation = Quaternion.Euler(0, angle, 0);
    //}
    #endregion
}