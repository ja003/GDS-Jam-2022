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
	[SerializeField] Button btnBackToMenu;
	[SerializeField] Animator MenuAnimator;

	private void Awake()
	{
		btnPlay.onClick.AddListener(Play);
		btnControls.onClick.AddListener(Controls);
		btnBackToMenu.onClick.AddListener(BackToMenu);
	}

	private void Play()
	{
		SceneManager.LoadScene("S_Game");
	}

	private void Controls()
	{
		MenuAnimator.Play("A_MenuToControls");
	}

	private void BackToMenu()
	{
		MenuAnimator.Play("A_ControlsToMenu");
	}
}
