using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTimer : MonoBehaviour
{
    public Text Timer;
    public int count=3;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
	public void TimerStart(){
		StartCoroutine(I_TimerStart());
	}
    IEnumerator I_TimerStart()
    {
		count = 3;
		Timer.text = count.ToString();
        while (true)
        {
			if(count ==0) break;
            yield return new WaitForSeconds(1f);
            count--;
			Timer.text = count.ToString();
        }
		OX_GM.instance.EndQuestion();
		// gameObject.SetActive(false);
    }
}
