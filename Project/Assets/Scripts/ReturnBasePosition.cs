using System.Collections;
using UnityEngine;

public class ReturnBasePosition : MonoBehaviour
{
    [SerializeField] private int playersOnPlatform = 0;
    [SerializeField] private float delayBeforeSlerp = 1.3f;
    private Quaternion basicRotation;
    private Rigidbody platformRB;
    [SerializeField] private bool isOutOfPlatform = false;


    void Start()
    {
        basicRotation = transform.rotation;

        platformRB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (playersOnPlatform == 0 && isOutOfPlatform)
        {
            StartCoroutine(ReturnPrevPositionWithDelay());
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            platformRB.isKinematic = false;
            playersOnPlatform++;
            isOutOfPlatform = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        playersOnPlatform--;
        isOutOfPlatform = true;
    }

    private IEnumerator ReturnPrevPositionWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeSlerp);
        platformRB.isKinematic = true;
        for (float i = 0; i < 5.0f; i += 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, basicRotation, 0.002f * Time.deltaTime);
            if (playersOnPlatform > 0)
                StopCoroutine(ReturnPrevPositionWithDelay());
            if (transform.rotation.x < 0.02f && transform.rotation.x > -0.02f)
            {
                StopCoroutine(ReturnPrevPositionWithDelay());
                platformRB.isKinematic = false;
                isOutOfPlatform = false;
            }
        }
    }
}
