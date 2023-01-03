using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System.Runtime.InteropServices;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProxyConnectServer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private InputField tokenInputField;
    [SerializeField] private InputField emailInputField;
    [SerializeField] private InputField APIInputField;

    public void Start()
    {
        emailInputField.text = "";
        APIInputField.text = "localhost:1234/";
    }

    public void OnErrorButtonClick()
    {
    }

    public void OnRunButtonClick()
    {
        StartCoroutine(EnuProxyServer(APIInputField.text, outputText));
    }

    public static IEnumerator EnuProxyServer(string APIRequest, TextMeshProUGUI outputText)
    {

        UnityWebRequest request = UnityWebRequest.Get(APIRequest);

        Debug.Log("about to send message");
        yield return request.SendWebRequest();
        Debug.Log("message sent");


        if (request.isNetworkError || request.isHttpError)
        {
            yield return request.error;
        }
        else
        {
            outputText.text = request.downloadHandler.text;
            string result = request.downloadHandler.text;
        }
    }


}

