using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIMove : MonoBehaviour
{
    public bool isAnswerCorrect = true;
    public string ans = "";
    int defaultDirection;
    
    // Use this for initialization
    void Start()
    {
        //오른쪽
        if (gameObject.name == "Character 1" || gameObject.name == "Character 2")
        {
            defaultDirection = 0;
        }
        //왼쪽
        else
        {
            Debug.Log("왼쪽보고있는" + gameObject.name);
            defaultDirection = 180;
        }
    }

    // Update is called once per frame
    void Update()
    {

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

    public void Move()
    {
        StartCoroutine(AI_Move());
    }
    IEnumerator AI_Move()
    {
        int random = Random.Range(-4, 4);
        random *= 50;
        if (random > 0)
        {
            random += 50;

            ans = "X";
        }
        else
        {
            random -= 50;
            ans = "O";
        }
        if (random > 0)
        {
            Debug.Log(gameObject.name + "오른쪽으로갑니다");
            transform.rotation = new Quaternion(0, 0 + defaultDirection, 0, 0);
        }
        else
        {
            if (defaultDirection == 180)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                Debug.Log(gameObject.name + "왼쪽으로갑니다");
                transform.rotation = new Quaternion(0, 180 + defaultDirection, 0, 0);
            }

        }
        yield return new WaitForSeconds(Random.Range(0, 1));


        iTween.MoveTo(gameObject, new Vector3(random, transform.position.y, transform.position.z), 4f);
        yield return new WaitForSeconds(3f);
    }
}
