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

	[SerializeField] CanvasGroup PauseMenuCanvas;
	[SerializeField] CanvasGroup ControlsCanvas;


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
		SetControlsInteractive(false);

		ControlsCanvas.alpha = 0;

		MenuAnimator.Play("A_PauseMenuOpen");

		game.HUD.Hide();
	}

	private void Resume()
	{
		MenuAnimator.Play("A_PauseMenuClose");
		game.HUD.Show();

	}

	private void ControlsToPause()
	{
		SetControlsInteractive(false);

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
		SetControlsInteractive(true);
		MenuAnimator.Play("A_PauseToControls");
	}

	private void SetControlsInteractive(bool pValue)
	{
		PauseMenuCanvas.interactable = !pValue;
		ControlsCanvas.interactable = pValue;
		ControlsCanvas.blocksRaycasts = pValue;
	}

	bool backToMenu;
	private void BackToMenu()
	{
		Resume();
		backToMenu = true;
	}
}
