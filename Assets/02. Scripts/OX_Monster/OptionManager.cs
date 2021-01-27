using System.Collections;
using UnityEngine;

public class OptionManager  : MonoBehaviour
{
    private static OptionManager Instance;
    public static OptionManager instance { get { return Instance; } }

	public GameObject quit;

	AudioSource bgmAudio;
	AudioSource rightAnswerAudio;
	AudioSource wrongAnswerAudio;

	public GameObject audioOn;
	public GameObject audioOff;

    void Awake()
    {
		bgmAudio = GameObject.Find("BGMAudio").GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;   
        }		
    }
    
	// Audio 설정 불러오기.
	void OnEnable(){
		if (PlayerPrefs.GetInt ("Audio", -1) == 0) {
			bgmAudio.mute = true;
			audioOn.SetActive (false);
			audioOff.SetActive (true);
		} else {
			bgmAudio.mute = false;
			audioOn.SetActive (true);
			audioOff.SetActive (false);
		}
	}

	// 게임종료.
	public void QuitGame(){
		Application.Quit ();
	}

	// Audio Off -> On
	public void AudioOn(){
		audioOn.SetActive (true);
		audioOff.SetActive (false);
		bgmAudio.mute = false;
		PlayerPrefs.SetInt ("Audio", 1);
	}

	// Audio On -> Off
	public void AudioOff(){
		audioOn.SetActive (false);
		audioOff.SetActive (true);
		bgmAudio.mute = true;
		PlayerPrefs.SetInt ("Audio", 0);
	}

	public void SetQuizEventAudio()
	{
		rightAnswerAudio = GameObject.Find("AnswerRightAudio").GetComponent<AudioSource>();
		wrongAnswerAudio = GameObject.Find("AnswerWrongAudio").GetComponent<AudioSource>();
	}
	public void PlayRightAnswerAudio()
	{
		rightAnswerAudio.Play();
	}

	public void PlayWrongAnswerAudio()
	{
		wrongAnswerAudio.Play();
	}
}