using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TMP_Text Text;
	
	public void SetScore(string text)
	{
		Text.text = text;
	}
}
