﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{


    public void startGame()
    {
        SceneManager.LoadScene("LevelSelectorScene", LoadSceneMode.Single);
    }
}
