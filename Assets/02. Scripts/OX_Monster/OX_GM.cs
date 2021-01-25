using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OX_GM : MonoBehaviour
{
    public GameObject Character;
    public List<GameObject> spectator;
    List<GameObject> failer;
    public GameObject O_Panel;
    public GameObject X_Panel;
    public GameObject GameOverPanel;
    public GameObject TruePanel;
    public GameObject FalsePanel;
    private static OX_GM Instance;
    
    private int totalQuizCount = 0;
    private int currentQuizCount = 0;

    public string ans;
    public static OX_GM instance { get { return Instance; } }
    bool isDelayTime = true;
    [SerializeField]
    List<Dictionary<string, object>> question = new List<Dictionary<string, object>>();

    int index = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        TruePanel = GameObject.Find("AnswerPanel_TRUE").transform.GetChild(0).gameObject;
        FalsePanel = GameObject.Find("AnswerPanel_FALSE").transform.GetChild(0).gameObject;

        totalQuizCount = APIHelper.instance.Get_quiz_totalCount();        
        Debug.Log("TotalQuestionCount : " + totalQuizCount);

        foreach (var item in APIHelper.instance.quizList)
        {
            var entry = new Dictionary<string, object>();
            entry["Question"] = item.problem;
            if(item.is_true){
                entry["answer"] = "O";
            }
            else{
                entry["answer"] = "X";
            }
            
            question.Add(entry);
        }
        StartCoroutine(delay());        
    }
    void Update()
    {

    }

    //플레이어가 O를 선택
    public void O_Choose()
    {
        ans = "O";
        //O 를 기록
    }

    //플레이어가 X를 선택
    public void X_Choose()
    {
        ans = "X";
        //X 를 기록
    }
    //문제시작
    public void StartQuestion()
    {
        TruePanel.SetActive(false);
        FalsePanel.SetActive(false);
        O_Panel.SetActive(false);
        X_Panel.SetActive(false);

        //문제타이머 set
        UI_M.instance.StartQuestion();
        index = Random.Range(0, totalQuizCount);
        UI_M.instance.SetQuestion(question[index]["Question"].ToString());
        isDelayTime = false;

        //UI_M에 문제 바꿔주고
        //캐릭터 이동가능하게
        Character.GetComponent<CharacterMove>().isMovable = true;
    }
    //StartQuestion 에서 켠 타이머가 꺼지면서 호출할거임.
    public void EndQuestion()
    {
        failer = new List<GameObject>();

        if (question[index]["answer"].ToString() == "O")
        {
            Debug.Log("정답" + question[index]["answer"].ToString());
            
            TruePanel.SetActive(true);
        }
        else
        {
            Debug.Log("정답" + question[index]["answer"].ToString());

            FalsePanel.SetActive(true);
        }

        if(question[index]["answer"] == ans)
        {
            UI_M.instance.AddScore(1);
        }
        UI_M.instance.AddTotalQuestionCount(1);

        //캐릭터 이동불가능하게
        Character.GetComponent<CharacterMove>().isMovable = false;        
        currentQuizCount++;

        
        if(currentQuizCount == totalQuizCount)
        {
            StartCoroutine(GameOver());
        }
        else
        {            
            StartCoroutine(delay());
        }
    }

    IEnumerator delay()
    {
        isDelayTime = true;
        yield return new WaitForSeconds(3);

        StartQuestion();
    }
    
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        TruePanel.SetActive(false);
        FalsePanel.SetActive(false);
        GameOverPanel.SetActive(true);
        GameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "Gameover\n Your highscore is : "+ UI_M.instance.Score.text+"\n";
        Character.GetComponent<CharacterMove>().isMovable = false;
        //Score 츨력
    }
    public void Lobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("00. Lobby");
    }
}
