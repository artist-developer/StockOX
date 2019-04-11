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
    public bool isMovable = false;
    public Vector3 gyroscope_rotation;

    float prevValue =0;
    // public Text Debug;
    void Awake()
    {
        go = GameObject.Find("Character");
        Input.gyro.enabled = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "O")
        {
            UI_M.instance.O_Choose();
        }
        else
        {
            UI_M.instance.X_Choose();
        }
    }
    void Update()
    {
        //자이로 
        gyroscope_rotation.x += Input.gyro.rotationRateUnbiased.x / 3;
        if(Mathf.Abs(Input.gyro.rotationRateUnbiased.y) > 0.001 )
        gyroscope_rotation.y += Input.gyro.rotationRateUnbiased.y / 3;
        
        
        if (isMovable && prevValue!=Input.gyro.rotationRateUnbiased.y)
        {
            if(gyroscope_rotation.y > 0 ){
                go.transform.rotation = new Quaternion(0,0,0,0);
            }else if(gyroscope_rotation.y < 0){
                go.transform.rotation = new Quaternion(0,180,0,0);
            }
            // 캐릭터 이동제한
            if (go.transform.position.x > -500 && go.transform.position.x < 500)
            {
                go.transform.position = new Vector3(go.transform.position.x + gyroscope_rotation.y, go.transform.position.y, go.transform.position.z);
                // go.transform.position = new Vector3(DistanceCalulation(gyroscope_rotation.y), go.transform.position.y, go.transform.position.z);
            }
            else
            {
                //-500 ~ 500 으로 x 포지션 제한을 두었지만 update특성상 소수점차이로 넘는 경우 발생하는 것을 방지
                if (go.transform.position.x < -500 && gyroscope_rotation.y > 0)
                {
                    go.transform.position = new Vector3(go.transform.position.x + gyroscope_rotation.y, go.transform.position.y, go.transform.position.z);
                    // go.transform.position = new Vector3(DistanceCalulation(gyroscope_rotation.y), go.transform.position.y, go.transform.position.z);
                }
                else if (go.transform.position.x > 500 && gyroscope_rotation.y < 0)
                {
                    go.transform.position = new Vector3(go.transform.position.x + gyroscope_rotation.y, go.transform.position.y, go.transform.position.z);
                    // go.transform.position = new Vector3(DistanceCalulation(gyroscope_rotation.y), go.transform.position.y, go.transform.position.z);
                }
            }
            //  if (go.transform.position.x > -500 && go.transform.position.x < 500)
            // {
            //     // go.transform.position = new Vector3(go.transform.position.x + gyroscope_rotation.y, go.transform.position.y, go.transform.position.z);
            //     go.transform.position = new Vector3(DistanceCalulation(gyroscope_rotation.y), go.transform.position.y, go.transform.position.z);
            // }
            // else
            // {
            //     //-500 ~ 500 으로 x 포지션 제한을 두었지만 update특성상 소수점차이로 넘는 경우 발생하는 것을 방지
            //     if (go.transform.position.x < -500 && gyroscope_rotation.y > 0)
            //     {
            //         // go.transform.position = new Vector3(go.transform.position.x + gyroscope_rotation.y, go.transform.position.y, go.transform.position.z);
            //         go.transform.position = new Vector3(DistanceCalulation(gyroscope_rotation.y), go.transform.position.y, go.transform.position.z);
            //     }
            //     else if (go.transform.position.x > 500 && gyroscope_rotation.y < 0)
            //     {
            //         // go.transform.position = new Vector3(go.transform.position.x + gyroscope_rotation.y, go.transform.position.y, go.transform.position.z);
            //         go.transform.position = new Vector3(DistanceCalulation(gyroscope_rotation.y), go.transform.position.y, go.transform.position.z);
            //     }
            // }
            prevValue = Input.gyro.rotationRateUnbiased.y;
        }
    }
    float DistanceCalulation(float gyro){
        float result=0f;
        if(gyro < -2.3 ) gyro = 2.3f;
        else if(gyro > 7.7){
            gyro = 7.7f;
        };

        gyro-=-2.3f;
        result = ((1000*gyro)/10)-500;
        //  -2.3       3.2       7.7  => 10
        //  -500       0         500  => 1000

        return result;
    }
}
