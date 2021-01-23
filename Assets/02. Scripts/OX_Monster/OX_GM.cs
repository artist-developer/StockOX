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
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    private static OX_GM Instance;
    
    public string ans;
    public static OX_GM instance { get { return Instance; } }
    bool isDelayTime = true;
    List<Dictionary<string, object>> question; //= CSVReader.Read("quiz");
    // string[] dummy = {
    //     "이순신 장군 동상은 오른손에 칼을 쥐고있다?",
    //     "달팽이도 이빨이 있다?",
    //     "셰익스피어 희곡 햄릿의 주인공인 햄릿은 네덜란드 사람이다?",
    //     "열대지방에 자라는 나무에는 나이테가 없다?",
    //     "세계 최초의 신용카드는 아메리칸 익스프레스이다?",
    //     "우리나라 최초의 대중가요는 1923년전부터 불려진 '희망가'이다." };
    // string[] dummyAns = { "O", "O", "X", "O", "X", "X" };

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
        question = CSVReader.Read("quiz");
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
        // Character.GetComponent<CharacterMove>().gyroscope_rotation = new Vector3(0,0,0);
        O_Panel.SetActive(false);
        X_Panel.SetActive(false);
        //문제타이머 set
        UI_M.instance.StartQuestion();
        index = Random.Range(0, 5);
        UI_M.instance.SetQuestion(question[index]["Question"].ToString());
        isDelayTime = false;

        //UI_M에 문제 바꿔주고

        //캐릭터 이동가능하게
        Character.GetComponent<CharacterMove>().isMovable = true;

        //들러리자식들 랜덤으로 포지션 옮겨
        foreach (GameObject g in spectator)
        {
            g.GetComponent<AIMove>().Move();
        }
    }
    //StartQuestion 에서 켠 타이머가 꺼지면서 호출할거임.
    public void EndQuestion()
    {
        failer = new List<GameObject>();

        if (question[index]["answer"].ToString() == "O")
        {
            Debug.Log("정답" + question[index]["answer"].ToString());
            X_Panel.SetActive(true);
        }
        else
        {
            Debug.Log("정답" + question[index]["answer"].ToString());
            O_Panel.SetActive(true);
        }
        if (question[index]["answer"].ToString() == ans)
        {
            UI_M.instance.AddScore(100);
            //캐릭터 이동불가능하게
            Character.GetComponent<CharacterMove>().isMovable = false;

            // Dropout();

            //딜레이가 필요한가

            StartCoroutine(delay());

            //틀린놈들 솎아내

            foreach (GameObject g in spectator)
            {
                if (g.GetComponent<AIMove>().ans != question[index]["answer"].ToString())
                {
                    failer.Add(g);
                }
            }
            foreach (GameObject g in failer)
            {
                Debug.Log(g.GetComponent<AIMove>().ans);
                spectator.Remove(g);
                Destroy(g);
            }
        }
        else
        {
            GameOver();
            
        }
    }
    IEnumerator delay()
    {
        isDelayTime = true;
        yield return new WaitForSeconds(3);

        StartQuestion();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        Character.GetComponent<CharacterMove>().isMovable = false;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        if (!isDelayTime)
            Character.GetComponent<CharacterMove>().isMovable = true;
    }
    //답 틀린 들러리들 떨쳐내기
    public void Dropout()
    {
        //List에 Quiz클래스를 들어야겠군

        //foreach돌려서 들러리놈들 클래스 하나 만들어서 bool값으로 맞았는가 틀렸는가 판별해서 죽여블자
    }
    public void GameOver()
    {
        // Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        GameOverPanel.transform.GetChild(0).GetComponent<Text>().text = "Gameover\n Your highscore is : "+UI_M.instance.Score.text+"\n";
        Character.GetComponent<CharacterMove>().isMovable = false;
        //Score 계산 , 순위비교 , 등
    }
    public void Lobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("00. Lobby");
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
