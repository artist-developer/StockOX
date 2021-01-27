using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class APIHelper  : MonoBehaviour
{
    static string ip = "http://54.180.153.40:3000";

    string companyList_url = ip + "/companyList";
    string price_url = ip + "/getProblem?company_id=";
    string news_url = ip + "/title?company_name=";
    string company_code = "none";
    string company_name = "none";

    public List<Company> cl = new List<Company>();
    public List<Quiz> quizList = new List<Quiz>();
    public List<CompanyNews> newsList = new List<CompanyNews>();

    private static APIHelper Instance;
    public static APIHelper instance { get { return Instance; } }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);            
        }
    }

    public string GetSelectedCompanyCode()
    {
        return company_code;
    }

    public int Get_quiz_totalCount()
    {        
        Debug.Log("quizList Count : " + quizList.Count);
        return quizList.Count;
    }

    public IEnumerator News_GetMethod()
    {
        newsList.Clear();
        using (UnityWebRequest webRequest = UnityWebRequest.Get(news_url + company_name))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                string s = webRequest.downloadHandler.text;
                string [] s_list = s.Substring(1, s.Length-2).Split('}');
                
                uint index = 0;
                foreach (var item in s_list)
                {
                    if(String.IsNullOrEmpty(item)) break ;
                    
                    if(index==0){
                        newsList.Add(JsonUtility.FromJson<CompanyNews>(item+"}"));
                    }
                    else{                                                
                        newsList.Add(JsonUtility.FromJson<CompanyNews>(item.Substring(1, item.Length-1)+"}"));
                    }
                    index++;
                }
            }
        }
    }

    public IEnumerator Quiz_GetMethod()
    {
        quizList.Clear();
        Debug.Log("Quiz_GetMethod started");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(price_url + company_code))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log(":\nQuiz_GetMethod Received: " + webRequest.downloadHandler.text);

                string s = webRequest.downloadHandler.text;
                string [] s_list = s.Substring(1, s.Length-2).Split('}');
                
                uint index = 0;
                foreach (var item in s_list)
                {
                    if(String.IsNullOrEmpty(item)) break ;
                    
                    if(index==0){
                        quizList.Add(JsonUtility.FromJson<Quiz>(item+"}"));
                    }
                    else{                                                
                        quizList.Add(JsonUtility.FromJson<Quiz>(item.Substring(1, item.Length-1)+"}"));
                    }
                    index++;
                }
            }
        }
    }

    public void CompanyList_GetMethod(){
        StartCoroutine(APIHelper.instance.CompanyList_couroutine());
    }

    public IEnumerator CompanyList_couroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(companyList_url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log("Get CompanyList : " + webRequest.error);
            }
            else
            {
                string s = webRequest.downloadHandler.text;    
                string [] s_list = s.Substring(1, s.Length-2).Split('}');
                
                uint index = 0;
                foreach (var item in s_list)
                {
                    if(String.IsNullOrEmpty(item)) break ;
                    
                    if(index==0){
                        cl.Add(JsonUtility.FromJson<Company>(item+"}"));
                    }
                    else{                                                
                        cl.Add(JsonUtility.FromJson<Company>(item.Substring(1, item.Length-1)+"}"));
                    }
                    index++;
                }
            Dropdown dropdown= GameObject.Find("Dropdown").GetComponent<Dropdown>();

	        List<string> dropdownOptions = new List<string>();
            foreach (var item in cl)
            {
                dropdownOptions.Add(item.companyName + " ("+item.companyId+")");
            }
        	dropdown.AddOptions(dropdownOptions);
            dropdown.onValueChanged.AddListener(delegate{DropdownValueChanged(dropdown);});
            }
        }                
    }
    
    void DropdownValueChanged(Dropdown change)
    {
        if(change.value ==0){
            company_code = "none";    
        }
        else{
            company_code = cl[change.value-1].companyId;
            company_name = cl[change.value-1].companyName;
            StartCoroutine(Quiz_GetMethod());
            StartCoroutine(News_GetMethod());
        }
    }
}

[System.Serializable]
public class Company
{
    public string companyId;
    public string companyName;
}

[System.Serializable]
public class Quiz
{
    public string problem;
    public bool is_true;
}

[System.Serializable]
public class CompanyNews
{
    public string title;
}