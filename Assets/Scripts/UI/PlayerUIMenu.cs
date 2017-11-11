﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerUIMenu : MonoBehaviour {

	void Update () {

        if (Input.GetButton("Player UI Menu"))
        {
            PlayerUIMenuManager.Instance.Repos();
        }
        else
        {
            PlayerUIMenuManager.Instance.GetComponentInChildren<Text>().text = "";
            PlayerUIMenuManager.Instance.MoveOut();
        }
        	
	}
}
