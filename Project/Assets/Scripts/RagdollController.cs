using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> ComponentsWithRigidbody;
    [SerializeField] private bool stateRagdoll;

    private Animator animatorComponent;
    private MoveController moveController;
    private Collider playerCollider;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        animatorComponent = GetComponent<Animator>();
        moveController = GetComponent<MoveController>();
        playerCollider = GetComponent<Collider>();
        playerRigidbody = GetComponent<Rigidbody>();

        ChangeStateRagdoll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && stateRagdoll)
        {
            stateRagdoll = false;
            ChangeStateRagdoll();
        }
        else if (Input.GetKeyDown(KeyCode.R) && !stateRagdoll)
        {
            stateRagdoll = true;
            ChangeStateRagdoll();
        }
    }

    private void ChangeStateRagdoll()
    {
        //animatorComponent.enabled = !stateRagdoll;
        moveController.enabled = !stateRagdoll;
        //playerCollider.enabled = !stateRagdoll;
        //playerRigidbody.isKinematic = stateRagdoll;

        foreach (Rigidbody element in ComponentsWithRigidbody)
        {
            element.GetComponent<Rigidbody>().isKinematic = !stateRagdoll;
            element.transform.GetComponent<Collider>().enabled = stateRagdoll;
        }
    }


}
