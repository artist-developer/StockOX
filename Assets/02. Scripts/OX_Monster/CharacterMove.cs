using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    public GameObject go;
    public Text t;
    public GameObject colider_O;
    public GameObject colider_X;
    public float speed = 300f;
    public bool isMovable = true;

    void Awake()
    {
        go = GameObject.Find("Character");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "O")
        {
            QuizManager.instance.O_Choose();
        }
        else
        {
            QuizManager.instance.X_Choose();
        }
    }
    void Update()
    {
        Vector3 pos = go.transform.position;
 
         if (Input.GetKey (KeyCode.UpArrow)) {
             if(pos.y <250)
             pos.y += speed * Time.deltaTime;
         }
         if (Input.GetKey (KeyCode.DownArrow)) {
             if(pos.y >-250)
             pos.y -= speed * Time.deltaTime;
         }
         if (Input.GetKey (KeyCode.RightArrow)) {
             if(pos.x <600)
             {
             pos.x += speed * Time.deltaTime;
             go.transform.localRotation = Quaternion.Euler(0, 0, 0);    
             }
             
         }
         if (Input.GetKey (KeyCode.LeftArrow)) {
             if(pos.x >-600){
pos.x -= speed * Time.deltaTime;
             go.transform.localRotation = Quaternion.Euler(0, 180, 0);
             }
             
         }             
 
         go.transform.position = pos;
    }
}
