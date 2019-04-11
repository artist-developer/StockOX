using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {
	private bool bStart = false;
	public Text startText;

	public AudioSource bgmAudio;
	public AudioSource effectAudio;
	public AudioClip countDownClip;

	// bStart값 초기화.
	void Start(){
		bStart = false;
	}

	// Audio 설정 불러오기.
	void OnEnable(){
		if (PlayerPrefs.GetInt ("Audio", -1) == 0) {
			bgmAudio.mute = true;
			effectAudio.mute = true;
		} else {
			bgmAudio.mute = false;
			effectAudio.mute = false;
		}
	}

	// 터치 계속해도 한번만 실행.
	public void StartGame(){
		if (bStart == false) {
			StartCoroutine (StartCount ());
			bStart = true;
		}
	}

	// 3초 후 씬 이동.
	private IEnumerator StartCount(){
		effectAudio.clip = countDownClip;
		effectAudio.Play ();
		startText.text = "3";
		yield return new WaitForSeconds (1.0f);
		effectAudio.clip = countDownClip;
		effectAudio.Play ();
		startText.text = "2";
		yield return new WaitForSeconds (1.0f);
		effectAudio.clip = countDownClip;
		effectAudio.Play ();
		startText.text = "1";
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene ("02. Tiltingmon");
	}
}
