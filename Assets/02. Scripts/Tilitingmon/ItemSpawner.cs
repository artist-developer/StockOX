using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour {
	public Transform[] points;
	private string objectName = "Item";
	private GameObject item;
	private int randomNum = 0;
	private int itemSelect = 0;

	private float itemSpawnSpeed = 2f;

	// itemSpawnSpeed 마다 코인 혹은 폭탄을 랜덤으로 생성.
	public IEnumerator ItemSpawn(){
		while (true) {
			yield return new WaitForSeconds (itemSpawnSpeed);
			if (MainManager.Instance.bGameOver == true)
				break;

			randomNum = UnityEngine.Random.Range (0, points.Length);
			item = ObjectPool.Instance.PopFromPool (objectName);
			item.transform.SetParent (points [randomNum]);
			item.transform.position = points [randomNum].localPosition;
			itemSelect = UnityEngine.Random.Range (6, 6);
			if (itemSelect == 0) {
				item.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Mace");
			} else {
				item.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Coin");
			}
			item.transform.localPosition = Vector3.zero;
			item.SetActive (true);
		}
	}
}
