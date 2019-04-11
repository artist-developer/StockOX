using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class GameOverCtrl : MonoBehaviour {
	private StringBuilder stb = new StringBuilder();
	
	public Text highScoreText;
	public Text TimerText;
	public Text GameoverText;

	// 게임오버 텍스트.
	void OnEnable(){
		stb.Length = 0;
		stb.Append ("GameOver\n");
		stb.Append (TimerText.text);
		stb.Append ("s\n");
		stb.Append ("Your highscore is : ");
		stb.Append (highScoreText.text);

		GameoverText.text = stb.ToString ();
	}
}
