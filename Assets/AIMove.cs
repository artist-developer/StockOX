using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour {
	public bool isAnswerCorrect = true;
	public string ans = "";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.name == "O")
    //     {
    //         ans="O";
    //     }
    //     else
    //     {
    //         ans="X";
    //     }
    // }

	public void Move(){
		StartCoroutine(AI_Move());
	}
	IEnumerator AI_Move(){
		int random = Random.Range(-4,4);
		random*=50;
		if(random>0){
			ans="X";
		}
		else{
			ans="O";
		}
		yield return new WaitForSeconds(Random.Range(0,1));
		iTween.MoveTo(gameObject,new Vector3(random, transform.position.y,transform.position.z),4f);
		yield return new WaitForSeconds(3f);
	}
}
