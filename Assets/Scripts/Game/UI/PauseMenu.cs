using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : GameBehaviour
{
	[SerializeField] Button btnResume;
	[SerializeField] Button btnControls;
	[SerializeField] Button btnMainMenu;
	[SerializeField] Animator MenuAnimator;

	private void Awake()
	{
		btnResume.onClick.AddListener(Resume);
		btnControls.onClick.AddListener(Controls);
		btnMainMenu.onClick.AddListener(BackToMenu);
	}

	public void Open()
	{
		gameObject.SetActive(true);
		Time.timeScale = 0;
		MenuAnimator.Play("A_PauseMenuOpen");
	}

	private void Resume()
	{
		MenuAnimator.Play("A_PauseMenuClose");
	}

	public void anim_OnMenuClosed()
	{
		Time.timeScale = 1; 
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
