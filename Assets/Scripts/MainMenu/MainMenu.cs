using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button btnPlay;    
    [SerializeField] Button btnControls;

    private void Awake()
    {
        btnPlay.onClick.AddListener(Play);
        btnControls.onClick.AddListener(Controls);
    }

    private void Play()
    {
        SceneManager.LoadScene("S_Game");
    }

    private void Controls()
    {
        SceneManager.LoadScene("S_Controls");
    }
}
