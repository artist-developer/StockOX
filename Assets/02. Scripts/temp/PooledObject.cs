using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PooledObject{
	public string poolItemName = string.Empty;
	public GameObject prefab = null;
	public int poolCount = 0;

	[SerializeField]
	private List<GameObject> poolList = new List<GameObject>();

	// 풀 List 초기화.
	public void Initialize(Transform parent = null) {
		for (int i = 0; i < poolCount; i++) {
			poolList.Add (CreateItem (parent));
		}
	}

	// 풀 List에 GameObject 반납.
	public void PushToPool(GameObject item, Transform parent = null){
		item.transform.SetParent (parent);
		item.SetActive (false);
		poolList.Add (item);
	}

	// 풀 List에서 GameObject 가져옴.
	public GameObject PopFromPool(Transform parent = null){
		if (poolList.Count == 0)
			poolList.Add (CreateItem (parent));

		GameObject item = poolList [0];
		poolList.RemoveAt (0);

		return item;
	}

	// 부족한 item 생성.
	private GameObject CreateItem(Transform parent = null){
		GameObject item = Object.Instantiate (prefab) as GameObject;
		item.name = poolItemName;
		item.transform.SetParent (parent);
		item.SetActive (false);

		return item;
	}
}
