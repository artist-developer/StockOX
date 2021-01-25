using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class APIHelper  : MonoBehaviour
{
    public string companyList_url = "http://54.180.26.193:8080/companyList";
    public string price_url = "http://3.36.75.50:3000/getProblem?company_id=";
    public string company_code = "none";
    // public List<Company> companyList = new List<Company>();
    // public Company[] companyList;
    public List<Company> cl = new List<Company>();
    public List<Quiz> quizList = new List<Quiz>();

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

    public void Set_companyCode(string _code)
    {
        company_code = _code;
    }

    public int Get_quiz_totalCount()
    {        
        Debug.Log("quizList Count : " + quizList.Count);
        return quizList.Count;
    }

    public IEnumerator Quiz_GetMethod()
    {
        quizList.Clear();
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
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

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
                // quizList = JsonUtility.FromJson<Quiz>(s);
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
                Debug.Log(webRequest.error);
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
                dropdownOptions.Add(item.company_name + " ("+item.company_id+")");
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
            company_code = cl[change.value-1].company_id;
            StartCoroutine(Quiz_GetMethod());
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