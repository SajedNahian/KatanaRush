﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayButton ()
    {
        Application.LoadLevel("gameplay");
    }

    public void QuitButton ()
    {
        Application.Quit();
    }

    public void AboutButton ()
    {
        Application.LoadLevel("about");
    }
}
