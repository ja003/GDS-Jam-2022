using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : CSingleton<Game>
{
	public Transform ProjectilesHolder;
	public Transform ProbesHolder;
	public Transform RewardsHolder;

	public Player Player;

    public HUD HUD;
	public EarthController Earth;

	[SerializeField] GameObject EndGameScreen;

    [SerializeField] Button btnPlay;
    [SerializeField] Button btnMenu;

    private void Awake()
    {
        EndGameScreen.SetActive(false);
        btnPlay.onClick.AddListener(PlayAgain);
        btnMenu.onClick.AddListener(Menu);
    }

    internal void EndGame()
	{
		HUD.OnEndGame();
		Debug.Log("End game");
		EndGameScreen.SetActive(true);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene("S_Game");
    }

    private void Menu()
    {
        SceneManager.LoadScene("S_Menu");
    }
}
