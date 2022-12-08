using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private float forceImpulse;
    private Transform Player;


    private void Update()
    {
        if (Player != null)
            Player.GetComponent<Rigidbody>().AddForce(transform.right * forceImpulse * Time.deltaTime, ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Вошёл");
            Player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = null;
        }
    }
}
