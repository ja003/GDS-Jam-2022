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
	public Transform AsteroidsHolder;

	public Player Player;

	public HUD HUD;
	[SerializeField] public PauseMenu PauseMenu;

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
		PauseMenu.gameObject.SetActive(false);

		Curtain.SetFade(false, () => { HasGameStarted = true; });
	}

	public bool IsInGame => !HasGameEnded && HasGameStarted;
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
		HUD.ShowMessage(EMessageType.GameOver);
	}

	private void PlayAgain()
	{
		Curtain.SetFade(true, () => { SceneManager.LoadScene("S_Game"); });
		//SceneManager.LoadScene("S_Game");
	}

	internal void PauseGame()
	{
		PauseMenu.Open();
	}

	private void Menu()
	{
		Curtain.SetFade(true, () => { SceneManager.LoadScene("S_Menu"); });

		//SceneManager.LoadScene("S_Menu");
	}
}
