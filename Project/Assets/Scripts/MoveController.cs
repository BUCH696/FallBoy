using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class MoveController : MonoBehaviour
{ 

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeResetFreeRotation;
    [SerializeField] private float timeStandUp;
    [SerializeField] private float timeoutUp;

    [SerializeField] private Transform _cameraCharacter;
    [SerializeField] private AnimationCurve animationCurveStandUp;
    private Rigidbody _rigidbody;
    private Animator Animator;

    public bool canMove = true;
    public bool isMove;
    public bool isFall;
    public bool inGround;
    public bool contactWithMechanism = false;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (GetVectorMove() == Vector3.zero || !canMove || contactWithMechanism) return;
        RotationCharacter();
        Move();
    }

    public Coroutine _status_corutin_up = null;
    public bool _status_collision_meh = false;
    public List<Collider> _status_collision_meh_list = new List<Collider>();

    private void Update()
    {
        Jump();
        isMove = GetVectorMove() == Vector3.zero ? false : true;
        SetAnimatorParametre("Run", isMove);
    }

    private void Move()
    {
        _rigidbody.MovePosition(transform.position + GetVectorMove() * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (inGround && Input.GetKeyDown(KeyCode.Space))
        {
            SetAnimatorParametre("Jump", true);
            _rigidbody.AddForce(GetVectorMove() + transform.up * jumpForce, ForceMode.VelocityChange);

        }else if(inGround && !Input.GetKey(KeyCode.Space))
        {
            SetAnimatorParametre("Jump", false);
        }
    }

    private Vector3 GetVectorMove()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (_cameraCharacter != null)
        {
            Vector3 cameraCharacter = Vector3.Scale(_cameraCharacter.forward, new Vector3(1, 0, 1));
            Vector3 mainCameraRight = Vector3.Scale(_cameraCharacter.right, new Vector3(1, 0, 1));

            Vector3 movementInCameraForwardDirection = cameraCharacter * verticalAxis;
            Vector3 movementInCameraRightDirection = mainCameraRight * horizontalAxis;

            Vector3 movementForward = movementInCameraForwardDirection + movementInCameraRightDirection;

            return new Vector3(movementForward.x, 0f, movementForward.z).normalized;
        }
        else
        {
            Vector3 movementForwardDirection = transform.forward * verticalAxis;
            Vector3 movementRightDirection = transform.forward * horizontalAxis;

            Vector3 movementForward = movementForwardDirection + movementRightDirection;
            return new Vector3(movementForward.x, 0f, movementForward.z).normalized;

        }
    }

    private void RotationCharacter()
    {
        if (_cameraCharacter == null) return;
        float targetAngle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + _cameraCharacter.transform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotationSpeed * Time.deltaTime);
    }

    private void CheckStayGround(Transform ground)
    {
        inGround = ground != null && ground.CompareTag("Ground") ? true : false;
    }

    private void SetAnimatorParametre(string nameParamentr, bool volume)
    {
        Animator.SetBool(nameParamentr, volume);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Mechanism")) 
        {
            this._status_collision_meh_list.Add(collision.collider);
            this._status_collision_meh = true;
        }

        if (this._status_collision_meh_list.Count > 0) 
        {
            if (this._status_corutin_up != null) 
                StopCoroutine(this._status_corutin_up);        

            Falling();
            Debug.Log(">>> >>> Уранил");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        CheckStayGround(collision.transform);
        //CheckContactWithMechanism(collision.transform);

    }

    private void OnCollisionExit(Collision collision)
    {
        CheckStayGround(null);
        CheckAndPlayStandUp(collision);
    }

    private void CheckAndPlayStandUp(Collision collision)
    {
        if (collision.transform.CompareTag("Mechanism"))
        {
            this._status_collision_meh = false;
            this._status_collision_meh_list.Remove(collision.collider);

            if (this._status_collision_meh_list.Count == 0)
            {
                this._status_corutin_up = StartCoroutine(StandUp());
                Debug.Log("Запустил StandUp");
            }
        }
    }

    private IEnumerator StandUp() 
    {

        yield return new WaitForSeconds(0.2f);

        while (!inGround)
        {
            Debug.Log("Жду землю");
            yield return new WaitForEndOfFrame();
        }

        if (inGround)
        {
            yield return new WaitForSeconds(timeoutUp - 0.2f);
            Debug.Log("Поднимаю");
            transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y, 0f), timeStandUp).OnComplete(() => { Debug.Log("Поднял"); ResetFalling();  }).SetEase(animationCurveStandUp);
        }

        yield return null;
    }
  
    public void Falling()
    {
        canMove = false;
        _rigidbody.freezeRotation = false;
        SetAnimatorParametre("Fall", true);
    }

    private void ResetFalling()
    {
        Debug.Log("Ресетнул");
        canMove = true;
        SetAnimatorParametre("Fall", false);
        _rigidbody.freezeRotation = true;
    }
    
}
