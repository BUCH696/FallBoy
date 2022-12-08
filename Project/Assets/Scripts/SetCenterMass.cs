using UnityEngine;

public class SetCenterMass : MonoBehaviour
{
    [SerializeField] Vector3 centerOfMass;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.Scale(centerOfMass, transform.localScale);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GetComponent<Rigidbody>().worldCenterOfMass, 0.1f);
    }
}
