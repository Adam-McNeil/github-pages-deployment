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

public class JiraConnect : MonoBehaviour
{
    // https://adam-mcneil.atlassian.net/rest/api/latest/issue/Test?-H 
    // baseURL = "https://adam-mcneil.atlassian.net/rest/";
    // getURL = "api/latest/search?name=yf";

    [SerializeField] private TextMeshProUGUI jiraText;
    [SerializeField] private TMP_InputField tokenInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField APIInputField;

    [DllImport("__Internal")]
    private static extern void Hello();

    public void OnErrorButtonClick()
    {
        Hello();
    }

    public void OnRunButtonClick()
    {
        MakeJiraRequestAsync(APIInputField.text, emailInputField.text, tokenInputField.text);
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

}

