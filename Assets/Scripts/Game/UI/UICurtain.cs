using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICurtain : GameBehaviour
{

	public void SetFade(bool pIn, Action pOnComplete, float pTime = 1)
	{
		gameObject.SetActive(true);
		int startAlpha = pIn ? 0 : 1;
		SetAlpha(startAlpha);
		int finalAlpha = pIn ? 1 : 0;
		LeanTween.alpha(image.rectTransform, finalAlpha, pTime)
			.setIgnoreTimeScale(true)
			.setOnComplete(pOnComplete);
	}
}
