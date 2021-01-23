using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool> {
	public List<PooledObject> objectPool = new List<PooledObject>();

	// 오브젝트풀 초기화.
	void Awake(){
		// for (int i = 0; i < objectPool.Count; i++) {
		// 	objectPool [i].Initialize (transform);
		// }
	}

	// 오브젝트풀에 itemName 반납.
	public bool PushToPool(string itemName, GameObject item, Transform parent = null){
		PooledObject pool = GetPoolItem (itemName);
		if (pool == null)
			return false;

		pool.PushToPool (item, parent == null ? transform : parent);
		return true;
	}

	// 오브젝트풀에서 itemName 가져옴.
	public GameObject PopFromPool(string itemName, Transform parent = null){
		PooledObject pool = GetPoolItem(itemName);
		if (pool == null)
			return null;

		return pool.PopFromPool (parent);		
	}

	// item 가져오기.
	PooledObject GetPoolItem(string itemName){
		for (int i = 0; i < objectPool.Count; i++) {
			if (objectPool [i].poolItemName.Equals (itemName))
				return objectPool [i];
		}
		return null;
	}
}
