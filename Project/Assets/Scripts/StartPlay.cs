using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartPlay : MonoBehaviour
{
    [SerializeField] List<Text> StartTexts;

    private void Start()
    {
        StartCoroutine(Start_play());
    }

    private IEnumerator Start_play()
    {
        foreach (var element in StartTexts)
        {

        }

        yield return null;
    }
}
