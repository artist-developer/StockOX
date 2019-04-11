using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MainManager : Singleton<MainManager> {
	public GameObject gameDisabled;
	
	public ItemSpawner itemSpawner;
	public Text TimerText;
	private float timer = 0f;
	public Text scoreText;
	public Text highScoreText;
	public GameObject gameoverAnim;

	public RectTransform character;
	public RectTransform land;

	private GameObject[] items;

	public bool bGameOver = false;
	private int nowScore = 0;
	private int i = 0;

	public bool bStartGame = false;

	public AudioSource bgmAudio;
	public AudioSource effectAudio;
	public GameObject audioOn;
	public GameObject audioOff;

	// Audio 설정 불러오기.
	void OnEnable(){
		if (PlayerPrefs.GetInt ("Audio", -1) == 0) {
			bgmAudio.mute = true;
			effectAudio.mute = true;
			audioOn.SetActive (false);
			audioOff.SetActive (true);
		} else {
			bgmAudio.mute = false;
			effectAudio.mute = false;
			audioOn.SetActive (true);
			audioOff.SetActive (false);
		}
	}

	// 점수 초기화 및 게임 시작.
	void Start(){
		scoreText.text = "0";
		highScoreText.text = PlayerPrefs.GetInt ("HighScore", 0).ToString();
		TimerText.text = "00.00";

		if (gameDisabled.activeInHierarchy == false) {
			GameStart ();
		}
	}

	// 시간 타이머 증가.
	void Update(){
		if (gameDisabled.activeInHierarchy == false && bGameOver == false) {
			timer += Time.deltaTime;
			TimerText.text = String.Format ("{0:f2}", timer);
		}
	}

	// 점수 초기화 및 아이템 스폰 시작.
	public void GameStart(){
		scoreText.text = "0";
		highScoreText.text = PlayerPrefs.GetInt ("HighScore", 0).ToString();

		StartCoroutine (itemSpawner.ItemSpawn ());
	}

	// 점수 증가.
	public void PlusScore(){
		scoreText.text = (Int32.Parse (scoreText.text) + 1).ToString();
	}

	// 게임오버 연출 및 최고점수 기록.
	public void GameOver(){
		bGameOver = true;
		Input.gyro.enabled = false;

		nowScore = Int32.Parse (scoreText.text);
		if (Int32.Parse (highScoreText.text) < nowScore) {
			highScoreText.text = nowScore.ToString();
			PlayerPrefs.SetInt ("HighScore", nowScore);
		}
		gameoverAnim.SetActive (true);
	}

	// 게임 재시작 및 위치 초기화.
	public void GameRestart(){
		gameoverAnim.SetActive (false);

		bGameOver = false;
		Input.gyro.enabled = true;

		character.SetPositionAndRotation (new Vector3(0f, -170f), new Quaternion(0f, 0f, 0f, 0f));
		land.SetPositionAndRotation (new Vector3(0f, -240f), new Quaternion(0f, 0f, 0f, 0f));
		items = GameObject.FindGameObjectsWithTag ("Item");
		for (i = 0; i < items.Length; i++) {
			ObjectPool.Instance.PushToPool ("Item", items[i]);
		}
		timer = 0f;

		GameStart ();
	}

	// 씬 이동.
	public void LobbyClicked(){
		SceneManager.LoadScene ("00. Lobby");
	}

	// Audio Off -> On
	public void AudioOn(){
		audioOn.SetActive (true);
		audioOff.SetActive (false);
		bgmAudio.mute = false;
		effectAudio.mute = false;
		PlayerPrefs.SetInt ("Audio", 1);
	}

	// Audio On -> Off
	public void AudioOff(){
		audioOn.SetActive (false);
		audioOff.SetActive (true);
		bgmAudio.mute = true;
		effectAudio.mute = true;
		PlayerPrefs.SetInt ("Audio", 0);
	}
}
