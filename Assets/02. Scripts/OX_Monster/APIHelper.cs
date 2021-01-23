using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIHelper
{
    public string companyList_url = "http://54.180.26.193:8080/companyList";
    public string price_url = "http://3.36.75.50:3000/getProblem?company_id=";
    public string company_code;
    public List<Company> companyList = new List<Company>();
    public List<Quiz> quizList = new List<Quiz>();

      private static OX_GM Instance;
          public static OX_GM instance { get { return Instance; } }
             void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            
        }
    }
    public void Set_companyCode(string _code)
    {
        company_code = _code;
    }

    public IEnumerator Quiz_GetMethod()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(price_url + company_code))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                string s = webRequest.downloadHandler.text;
                quizList = JsonUtility.FromJson<Quiz>(s);
            }
        }
    }

    public IEnumerator CompanyList_GetMethod()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(companyList_url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                string s = webRequest.downloadHandler.text;
                companyList = JsonUtility.FromJson<Company>(s);
            }
        }
    }
}

[System.Serializable]
public class Company
{
    public string company_id;
    public string company_name;
}

[System.Serializable]
public class Quiz
{
    public string problem;
    public bool is_true;
}