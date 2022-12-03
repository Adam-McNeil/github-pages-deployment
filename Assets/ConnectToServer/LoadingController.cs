using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingController : MonoBehaviour
{
    private TextMeshProUGUI loadingText;
    private int periodCount;
    private string baseString = "Loading";

    void Start()
    {
        loadingText = GetComponent<TextMeshProUGUI>(); 
        periodCount = 0;
        loadingText.text = baseString;
        StartCoroutine(Loading());
    }
    
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(0.5f);
        periodCount++;
        periodCount %= 4;
        string temp = baseString;
        for (int i = 0; i < periodCount; i++)
        {
            temp = temp + ".";
        }
        loadingText.text = temp;
        StartCoroutine(Loading());
    }
}
