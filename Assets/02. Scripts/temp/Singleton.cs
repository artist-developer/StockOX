using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	
	protected static T instance = null;

	// Instance가 생성되지 않았다면 생성.
	public static T Instance
	{
		get
		{
			instance = FindObjectOfType (typeof(T)) as T;

			if (instance == null)
			{
				instance = new GameObject ("@"+typeof(T).ToString (),
					typeof(T)).GetComponent<T>();
				DontDestroyOnLoad (instance);
			}
			return instance;
		}
	}
}