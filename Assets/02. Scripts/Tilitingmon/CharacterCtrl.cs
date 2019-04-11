using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCtrl : MonoBehaviour {
	public AudioSource effectAudio;
	public AudioClip coinClip;
	public AudioClip maceClip;
	
	// 캐릭터가 코인을 먹었는지 폭탄과 맞았는지 체크.
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.CompareTag ("Item")) {
			if (coll.GetComponent<Image> ().sprite.name == "Mace") {
				effectAudio.clip = maceClip;
				effectAudio.Play ();
				MainManager.Instance.GameOver ();	
			} else {
				effectAudio.clip = coinClip;
				effectAudio.Play ();
				MainManager.Instance.PlusScore ();
				ObjectPool.Instance.PushToPool ("Item", coll.gameObject);
			}
		}
	}
}
