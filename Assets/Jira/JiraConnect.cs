using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class JiraConnect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI jiraText;

    private void Start()
    {
        // https://adam-mcneil.atlassian.net/rest/api/latest/issue/Test?-H \"Authorization: HebzeuStL2EU43ulUo46C682\"

        string baseURL = "https://adam-mcneil.atlassian.net/rest/";
        string loginUsername = "adamwmcneil@gmail.com";
        //string loginPassword = "something";
        string loginToken = "ZCwd7guPdWjfJyobV0cE3F49";
        // string loginAPI = "auth/latest/session/";
        string getURL = "api/latest/search?name=yf";


        MakeJiraRequest(baseURL + getURL, loginUsername, loginToken);
        //MakeJiraTicket(baseURL + getURL, loginUsername, loginToken);
        Console.ReadLine();
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

        /*   WebRequest responseRequest = WebRequest.Create(URL);
           responseRequest.Headers.Add("Authorization", "Basic " + encodedStr);
           responseRequest.Method = "GET";
           WebResponse webResponse = responseRequest.GetResponse();
           Stream responseStream = webResponse.GetResponseStream();
           StreamReader reader = new StreamReader(responseStream);
           string response = reader.ReadToEnd();
           Console.Write(response);*/
    }

    private void MakeJiraRequest(string URL, string username, string token)
    {
        // make data request
        WebRequest issueRequest = WebRequest.Create(URL);
        string encodedStr = Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(username + ":" + token));
        issueRequest.Headers.Add("Authorization", "Basic " + encodedStr);
        issueRequest.Method = "GET";
        WebResponse webResponse = issueRequest.GetResponse();
        Stream responseStream = webResponse.GetResponseStream();
        StreamReader reader = new StreamReader(responseStream);
        string response = reader.ReadToEnd();
        jiraText.text = response;
        Debug.Log(response);
    }
}

