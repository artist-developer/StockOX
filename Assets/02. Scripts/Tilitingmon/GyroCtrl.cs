using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroCtrl : MonoBehaviour {
	public float gyroRotateSpeed;
	private float beforeTiltAngle = 0f;
	private float tiltAngle = 0f;
	private float tempFloat = 0f;
	private float tempY = 0f;
	public GameObject playerController;
	public RectTransform playerTransform;
	public GameObject land;

	// 자이로가 켜져있으면 GyroStart 지속.
	void Update(){
		if (Input.gyro.enabled) {
			GyroStart ();
		}
	}

	// tiltAngle(경사각)을 받아와 Land, Player의 위치와 각도 조정.
	private void GyroStart(){
		tiltAngle = GetDeviceTiltAngle();
		tempFloat = Mathf.LerpUnclamped(0f, 5f, tiltAngle * -1f);//(a,b,t) when t=0 return a, when t=1 return b
		land.transform.eulerAngles = new Vector3(0f, 0f, tempFloat);

		if (tiltAngle < beforeTiltAngle - 0.02f) {
			playerTransform.rotation = new Quaternion (0f, -180f, 0f, 0f);
		} else if (tiltAngle > beforeTiltAngle + 0.02f) {
			playerTransform.rotation = new Quaternion (0f, 0f, 0f, 0f);
		}

		tempFloat = 0f;
		tempFloat = Mathf.LerpUnclamped(0f, 300f, tiltAngle);
		if (tiltAngle > 0) {
			tempY = 0f;
			tempY = Mathf.LerpUnclamped (-170f, -200f, tiltAngle);
		} else {
			tempY = 0f;
			tempY = Mathf.LerpUnclamped (-170f, -200f, -tiltAngle);
		}
		Vector3 direction = new Vector3(tempFloat, tempY, playerController.transform.localPosition.z);
		playerController.transform.localPosition = direction;

		beforeTiltAngle = tiltAngle;
	}

	// Yaw 각도에서 최대치, 최소치를 제한.
	public float GetDeviceTiltAngle()
	{
		tempFloat = this.getYawAngle();
		tempFloat = Mathf.Clamp(tempFloat, -10f, 10f);
		tempFloat /= 10f;
		return tempFloat;
	}

	// 자이로 상태로부터 Yaw 각도를 얻음.
	public float getYawAngle()
	{
		Quaternion attitude = Input.gyro.attitude;
		float num = Mathf.Asin(2f * (attitude.x * attitude.z - attitude.w * attitude.y));//asin역사인값 반환
		num = num * 180f / 3.14159274f;
		return (num - 0) * -1f;
	}
}