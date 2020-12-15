using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Net.Http;
using iBoost;
using System.Text;

public enum QueryType
{
    GET_LOGIN,
    GET_RENEW,
}


public class LiveAPI : MonoBehaviour
{
    #region Public Property
    private static LiveAPI _Instance;
    public static LiveAPI Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = new GameObject().AddComponent<LiveAPI>();
            }

            return _Instance;
        }
    }
    #endregion


    #region Private Property
    //private const string HOST = "https://api.cloud.wowza.com";
    private const string HOST = "http://34.87.94.243/app/api/v1/login.php";
    //private const string VERSION = "v1.4";
    private const string VERSION = "v2";
    private const string API_KEY = "XAVPF6BLZ1wJY9UpMcE8XmpNjX4x8V9OoklQRhm8gAWyOQZc5RiE9XfJS1rP321f";
    private const string ACCESS_KEY = "TeziPkCgCJ712ENu1T0wSDkkELSgSwxcZkH3KvGjV9RR4Ps8Q5p9wiHFmNMy3263";
    //private static string URL { get { return string.Format("{0}/api/{1}/live_streams", HOST, VERSION); } }
    private static string URL { get { return string.Format("{0}/api/{1}/live_streams", HOST, VERSION); } }
    #endregion


    #region Monobehavior Callback    
    private void Awake()
    {
        //Instance = this;
    }
    #endregion



    #region Public Method
    public void Query(QueryType type, string param, Action<string, string> callback, string body = null)
    {
        switch(type)
        {

            // GET
            case QueryType.GET_LOGIN: 
                {
                    StartCoroutine(BaseQuery("GET", HOST + "/login.php?" + param , callback, body));
                    break;
                }
            // GET
            case QueryType.GET_RENEW: 
                {
                    StartCoroutine(BaseQuery("GET", HOST + "/renew.php?" + param , callback, body));
                    break;
                }
        }
    }
    #endregion



    #region Private Method
    private IEnumerator BaseQuery(string method, string url, Action<string, string> callback, string body = null)
    {
        Log.Info("Start Query: " + url);

        UnityWebRequest request = new UnityWebRequest(url, method);
        request.downloadHandler = new DownloadHandlerBuffer();

        if(PlayerData.loginData != null)
        {
            request.SetRequestHeader("Authorization", PlayerData.loginData.data.token.access.token);
        }

        request.SetRequestHeader("Content-Type", @"application/json");

        if(body != null)
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        yield return request.SendWebRequest();

        if(request.isNetworkError || !request.responseCode.ToString().StartsWith("2"))
        {
            Log.Error("Error: " + request.error + " & " + request.downloadHandler.text);
            callback?.Invoke("", request.error);
        }
        else
        {
            //Debug.Log(request.);
            Log.Success("Success: " + request.downloadHandler.text);
            callback?.Invoke(request.downloadHandler.text, null);
        }

        request.Dispose();
    }
    #endregion
}
