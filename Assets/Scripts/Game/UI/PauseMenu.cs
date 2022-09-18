using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : GameBehaviour
{
	[SerializeField] Button btnResume;
	[SerializeField] Button btnControls;
	[SerializeField] Button btnMainMenu;
	[SerializeField] Animator MenuAnimator;
	[SerializeField] Button btnBackFromControls;

	private void Awake()
	{
		btnResume.onClick.AddListener(Resume);
		btnControls.onClick.AddListener(Controls);
		btnMainMenu.onClick.AddListener(BackToMenu);
		btnBackFromControls.onClick.AddListener(ControlsToPause);
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

	private void ControlsToPause()
	{
		MenuAnimator.Play("A_ControlsToPause");
	}

	public void anim_OnMenuClosed()
	{
		Time.timeScale = 1;
		if(backToMenu)
		{
			SceneManager.LoadScene("S_Menu");
		}
	}

	private void Controls()
	{
		MenuAnimator.Play("A_PauseToControls");
	}

	bool backToMenu;
	private void BackToMenu()
	{
		Resume();
		backToMenu = true;
	}
}
