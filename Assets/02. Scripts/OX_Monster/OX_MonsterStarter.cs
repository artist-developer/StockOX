﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OX_MonsterStarter : MonoBehaviour {
	
	
	void Start () {
		APIHelper.instance.CompanyList_GetMethod();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OX_MonsterGame(){
		if(APIHelper.instance.GetSelectedCompanyCode() !="none"){
			SceneManager.LoadScene("03. OX_Monster");
		}
		
	}
}
