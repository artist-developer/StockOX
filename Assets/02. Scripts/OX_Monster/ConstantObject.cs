using UnityEngine;
using System.Collections;
 
public class ConstantObject : MonoBehaviour {

    private static APIHelper Instance;

    void Awake() {
        
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
