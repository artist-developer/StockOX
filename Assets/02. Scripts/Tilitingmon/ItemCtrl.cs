using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour {
	private string poolItemName = "Item";
	private float t = 0f;
	private int itemFallingSpeed = 10;

	// 시간 초기화.
	void OnEnable(){
		t = 0f;
	}

	// itemFallingSpeed 속도로 5f 동안 낙하.
	void Update () {
		if (MainManager.Instance.bGameOver == false) {
			t += Time.deltaTime / 1.0f;

			transform.position += Vector3.down * itemFallingSpeed;

			if (t > 5f) {
				t = 0f;
				this.gameObject.SetActive (false);
				this.transform.localPosition = Vector3.zero;
				ObjectPool.Instance.PushToPool (poolItemName, gameObject);
			}
		}
	}
}
