using System.Collections.Generic;
using UnityEngine;

public class AddForceInDirection : MonoBehaviour
{
    [SerializeField] private float volumeForce;
    [SerializeField] private List<Transform> TriggerObject;


    private void FixedUpdate()
    {
        AddForceToObject();
    }

    private void AddForceToObject()
    {
        if (TriggerObject.Count <= 0) return;

        foreach (Transform element in TriggerObject)
        {
            if (element.GetComponent<Rigidbody>())
                element.GetComponent<Rigidbody>().AddForce(transform.up * volumeForce * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerObject.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerObject.Remove(other.transform);
    }
}
