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
	public ScienceController Earth;

	[SerializeField] UICurtain Curtain;
	[SerializeField] GameObject EndGameScreen;

	[SerializeField] Button btnPlay;
	[SerializeField] Button btnMenu;

	private void Awake()
	{
		EndGameScreen.SetActive(false);
		btnPlay.onClick.AddListener(PlayAgain);
		btnMenu.onClick.AddListener(Menu);

		HUD.Init();
		Curtain.SetFade(false, () => { HasGameStarted = true; });
	}

	public bool IsInGame => !HasGameEnded && !HasGameStarted;
	bool HasGameEnded;
	bool HasGameStarted;

	internal void EndGame()
	{
		if(HasGameEnded)
			return;

		HasGameEnded = true;

		HUD.OnEndGame();
		Debug.Log("End game");
		EndGameScreen.SetActive(true);
	}

	private void PlayAgain()
	{
		Curtain.SetFade(true, () => { SceneManager.LoadScene("S_Game"); });
		//SceneManager.LoadScene("S_Game");
	}

	private void Menu()
	{
		Curtain.SetFade(true, () => { SceneManager.LoadScene("S_Menu"); });

		//SceneManager.LoadScene("S_Menu");
	}
}
