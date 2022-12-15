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

public class JiraConnect : MonoBehaviour
{
    // https://adam-mcneil.atlassian.net/rest/api/latest/issue/Test?-H 
    // baseURL = "https://adam-mcneil.atlassian.net/rest/";
    // getURL = "api/latest/search?name=yf";

    [SerializeField] private TextMeshProUGUI jiraText;
    [SerializeField] private TMP_InputField tokenInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField APIInputField;



    public void OnErrorButtonClick()
    {
        //Hello();
    }

    public void OnRunButtonClick()
    {
        StartCoroutine(EnuJira(tokenInputField.text, emailInputField.text, APIInputField.text, jiraText, returnValue =>
        {
           
        }
        ));
        
    }

    private void MakeJiraTicket(string URL, string username, string token)
    {
        string data = Console.ReadLine();
        WebRequest request = WebRequest.Create(URL);
        string encodedStr = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + token));
        byte[] byteArray = Encoding.UTF8.GetBytes(data);
        request.Headers.Add("Authorization", "Basic " + encodedStr);
        request.ContentType = "application/json";
        request.ContentLength = byteArray.Length;
        request.Method = "POST";
        Stream stream = request.GetRequestStream();
        stream.Write(byteArray, 0, byteArray.Length);
    }

    private async void MakeJiraRequestAsync(string URL, string username, string token)
    {
        // make data request
        try
        {
            WebRequest issueRequest = WebRequest.Create(URL);
            string encodedStr = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + token));
            issueRequest.Headers.Add("Authorization", "Basic " + encodedStr);
            issueRequest.Method = "GET";
            WebResponse webResponse = await Task.Run(() => issueRequest.GetResponse());
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string response = reader.ReadToEnd();
            jiraText.text = response;
            Debug.Log(response);
        }
        catch (Exception ex)
        {
            jiraText.text = ex.Message;
        }
    }

    public static IEnumerator EnuJira(string token, string usename, string APIRequest, TextMeshProUGUI outputText, System.Action<string> callback = null)
    {
        string authCache = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(usename + ":" + token));

        UnityWebRequest request = UnityWebRequest.Get(APIRequest);
        request.SetRequestHeader("Authorization", "Basic " + authCache);
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
           /* Debug.Log("Error while Receiving: " + request.error);
            string result = request.downloadHandler.text;
            callback.Invoke(result);*/
            yield return request.error;
            callback.Invoke(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            outputText.text = request.downloadHandler.text;
            string result = request.downloadHandler.text;
            callback.Invoke(result);
        }
    }

/*    private void CallJavaScriptFunction(string URL, string username, string token)
    {
        string encodedStr = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + token));
        MakeJiraRequestJavaScript(URL, encodedStr);
    }*/

}

