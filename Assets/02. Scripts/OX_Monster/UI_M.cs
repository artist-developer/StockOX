using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_M : MonoBehaviour
{
    public Text question;
    public Text Score;
    public GameObject QuizTimer;
    public GameObject O_PanelBlur;
    public GameObject X_PanelBlur;
    public GameObject Audio;
    bool isAudioOn =true;
    public Text AudioText;
    public float time;
    int dummyCount = 0;

    int currentScore = 0;
    int currentTotalQuestionCount;

    //싱글톤
    private static UI_M Instance;
    public static UI_M instance { get { return Instance; } }

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
        Score.text = currentScore + " / " + currentTotalQuestionCount;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //문제변경
    public void SetQuestion(string s)
    {
        question.text = s;
    }

    //점수 초기화할때나 필요할때 쓸거임
    public void SetScore(string s)
    {
        Score.text = s;
    }

    //현재 총 문제 수
    public void AddTotalQuestionCount(int num)
    {        
        currentTotalQuestionCount = currentTotalQuestionCount + num;

        Score.text = currentScore + " / " + currentTotalQuestionCount;
    }

    //점수더하기 게임매니저에서 불러서 쓸거임
    public void AddScore(int num)
    {
        currentScore += num;
        Score.text = currentScore + " / " + currentTotalQuestionCount;
    }

    //플레이어가 O를 선택
    public void O_Choose()
    {
		OX_GM.instance.O_Choose();
        O_PanelBlur.SetActive(true);
        X_PanelBlur.SetActive(false);
    }

    //플레이어가 X를 선택
    public void X_Choose()
    {
		OX_GM.instance.X_Choose();
        O_PanelBlur.SetActive(false);
        X_PanelBlur.SetActive(true);
    }

    public void StartQuestion()
    {
        QuizTimer.GetComponent<QuizTimer>().count = 3;
        QuizTimer.GetComponent<QuizTimer>().TimerStart();
    }

    public void EndQuestion()
    {
    }

    public void AudioToggle(){
        isAudioOn=!isAudioOn;
        if(isAudioOn){
            Audio.SetActive(true);
            AudioText.text = "Sound Off";
        }
        else{
            Audio.SetActive(false);
            AudioText.text = "Sound On";
        }
    }
}
