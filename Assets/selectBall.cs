using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectBall : MonoBehaviour {
    public List<GameObject> ballList;
	// Use this for initialization
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onSelectBall(int k)
    {
        foreach(GameObject c in ballList)
        {
            if (c == ballList[k])
            {
                c.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                c.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
