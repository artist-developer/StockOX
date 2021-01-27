using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour {
	public GameObject quit;

	public AudioSource bgmAudio;
	public GameObject audioOn;
	public GameObject audioOff;

	// 화면 비율, 프레임 고정.
	void Awake(){
		Screen.SetResolution(1280, 720, true);
		Application.targetFrameRate = 60;
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

	// 씬 이동.
	public void OX_MonsterGame(){
		SceneManager.LoadScene("03. OX_MonsterStart");
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
}
