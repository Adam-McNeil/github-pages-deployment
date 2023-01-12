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
using UnityEditor.Experimental.GraphView;

public class ProxyConnectServer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI outputText;
    [SerializeField] private InputField tokenInputField;
    [SerializeField] private InputField emailInputField;
    [SerializeField] private InputField APIInputField;

    private static string proxyServer = "http://192.168.1.20:7777/jira-request/"; 

    public void Start()
    {
        emailInputField.text = "";
        APIInputField.text = "";
    }

    public void OnErrorButtonClick()
    {
    }

    public void OnRunButtonClick()
    {
        StartCoroutine(EnuProxyServer(APIInputField.text, outputText));
    }

    public static IEnumerator EnuProxyServer(string jiraAPIEndpoint, TextMeshProUGUI outputText)
    {
        Debug.Log(jiraAPIEndpoint);
        UnityWebRequest request = UnityWebRequest.Post(proxyServer, "{\"endpoint\":\"" + jiraAPIEndpoint + "\"}");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

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

