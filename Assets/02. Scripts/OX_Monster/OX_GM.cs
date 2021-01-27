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

    [SerializeField]
    List<Dictionary<string, object>> question = new List<Dictionary<string, object>>();

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
        //Debug.Log("QuizCount : " + totalQuizCount);
        // if(totalQuizCount == 0)
        // {
        //     StartCoroutine(GameOver());
        // }

        //else
        //{
            TruePanel = GameObject.Find("AnswerPanel_TRUE").transform.GetChild(0).gameObject;
            FalsePanel = GameObject.Find("AnswerPanel_FALSE").transform.GetChild(0).gameObject;

            totalQuizCount = APIHelper.instance.Get_quiz_totalCount();

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

            OptionManager.instance.SetQuizEventAudio();

            StartCoroutine(delay());
        //}
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
        QuizManager.instance.StartQuestion();
        QuizManager.instance.SetQuestion(question[currentQuizCount]["Question"].ToString());

        //QuizManager에 문제 바꿔주고
        //캐릭터 이동가능하게
        Character.GetComponent<CharacterMove>().isMovable = true;
    }
    //StartQuestion 에서 켠 타이머가 꺼지면서 호출할거임.
    public void EndQuestion()
    {
        failer = new List<GameObject>();

        //정답!
        if(question[currentQuizCount]["answer"].ToString() == ans)
        {
            TruePanel.SetActive(true);
            OptionManager.instance.PlayRightAnswerAudio();
            QuizManager.instance.AddScore(1); 
        }
        //오답!
        else
        {
            OptionManager.instance.PlayWrongAnswerAudio();
            FalsePanel.SetActive(true);
        }
        QuizManager.instance.AddTotalQuestionCount(1);

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
        yield return new WaitForSeconds(3);

        StartQuestion();
    }
    
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);

        Character.GetComponent<CharacterMove>().isMovable = false;
        TruePanel.SetActive(false);
        FalsePanel.SetActive(false);
        
        string news_info = "";
        foreach (var item in APIHelper.instance.newsList)
        {
            news_info += item.title;
            news_info += "\n";
        }

        GameOverPanel.transform.GetChild(0).GetComponent<Text>().text 
            = "Your highscore is : "+ QuizManager.instance.Score.text + "\n\n" 
            + "<기업 관련 뉴스>\n" + news_info;
        GameOverPanel.transform.GetChild(0).GetComponent<Text>().fontSize = 25;
        
        yield return new WaitForSeconds(1);

        GameOverPanel.SetActive(true);
    }
    public void Lobby()
    {
        Time.timeScale = 1;
		SceneManager.LoadScene("03. OX_MonsterStart");
    }
}
