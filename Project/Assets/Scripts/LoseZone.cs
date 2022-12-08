using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseZone : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Transform SpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform Player = other.transform;
            Player.position = SpawnPoint.position;
        }
    }

}
