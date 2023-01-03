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

public class JiraConnect : MonoBehaviour
{
    // https://adam-mcneil.atlassian.net/rest/api/latest/issue/Test?-H 
    // baseURL = "https://adam-mcneil.atlassian.net/rest/";
    // getURL = "api/latest/search?name=yf";

    [SerializeField] private TextMeshProUGUI jiraText;
    [SerializeField] private InputField tokenInputField;
    [SerializeField] private InputField emailInputField;
    [SerializeField] private InputField APIInputField;
    private string resultCode;

    /// 
    //https://auth.atlassian.com/authorize?
    //audience=api.atlassian.com&
    //client_id=FfkyEIIr4kd4acglG27fROkFl2scCKZK&
    //scope=REQUESTED_SCOPE_ONE%20REQUESTED_SCOPE_TWO&
    //redirect_uri=https://github.com/&
    //
    //response_type=code&
    //prompt=consent


        //https://auth.atlassian.com/authorize?audience=api.atlassian.com&client_id=FfkyEIIr4kd4acglG27fROkFl2scCKZK&scope=read%3Ame&redirect_uri=https%3A%2F%2Fgithub.com%2F&state=${YOUR_USER_BOUND_VALUE}&response_type=code&prompt=consent

    public void Start()
    {
        emailInputField.text = "adamwmcneil@gmail.com";
        APIInputField.text = "https://adam-mcneil.atlassian.net/rest/api/latest/search?name=yf";
    }

    public void OnErrorButtonClick()
    {
        //Hello();
    }

    public void OnRunButtonClick()
    {
        StartCoroutine(Unity_Jira_Connect());
        Debug.Log(resultCode);
        //StartCoroutine(EnuJira(tokenInputField.text, emailInputField.text, APIInputField.text, jiraText, returnValue =>
        //{
        //   
        //   
        //}
        //)); 
    }

    private IEnumerator Unity_Jira_Connect()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://auth.atlassian.com/authorize?audience=api.atlassian.com&client_id=FfkyEIIr4kd4acglG27fROkFl2scCKZK&scope=read%3Ame&redirect_uri=https%3A%2F%2Fadam-mcneil.github.io%2Fgithub-pages-deployment%2F&state=${YOUR_USER_BOUND_VALUE}&response_type=code&prompt=consent");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            yield return request.error;
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            resultCode = request.downloadHandler.text;
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

    struct PostData
    {
        // { 
        //     "grant_type": "authorization_code",
        //     "client_id": "YOUR_CLIENT_ID",
        //     "client_secret": "YOUR_CLIENT_SECRET",
        //     "code": "YOUR_AUTHORIZATION_CODE",
        //     "redirect_uri": "https://adam-mcneil.github.io/github-pages-deployment/"
        // }
        string grant_type;
        string client_id;
        string client_secret;
        string code;
        string redirect_uri;

        PostData(string gt, string ci, string cs, string c, string ru)
        {
            grant_type = gt;
            client_id = ci;
            client_secret = cs;
            code = c;
            redirect_uri = ru;
        }
    }

    #region Old_Requests
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
    #endregion


/*    private void CallJavaScriptFunction(string URL, string username, string token)
    {
        string encodedStr = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + token));
        MakeJiraRequestJavaScript(URL, encodedStr);
    }*/

}

