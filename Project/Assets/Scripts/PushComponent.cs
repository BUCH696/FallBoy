using System.Collections.Generic;
using UnityEngine;

public class PushComponent : MonoBehaviour
{
    [SerializeField] private List<Transform> CollisionObject;
    [SerializeField] private List<AudioClip> PuchAudioClipsList;

    [SerializeField] private float push_force = 1f;

    private AudioSource AudioSource;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void AddPushForceAndPlaySound(Vector3 direction)
    {
        foreach (var triggerObject in CollisionObject)
        {
            triggerObject.GetComponent<Rigidbody>().AddForce(direction * push_force, ForceMode.VelocityChange);
            AudioSource.PlayOneShot(PuchAudioClipsList[Random.Range(0, PuchAudioClipsList.Count)]);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Rigidbody>())
        {
            CollisionObject.Add(collision.transform);
            Vector3 directionPointCollision = (collision.transform.position - transform.position).normalized;
            AddPushForceAndPlaySound(directionPointCollision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.GetComponent<Rigidbody>())
        {
            CollisionObject.Remove(collision.transform);
        }
    }
}
