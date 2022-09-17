using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : CSingleton<Game>
{
	public Transform ProjectilesHolder;
	public Transform ProbesHolder;
	public Transform RewardsHolder;

	public Player Player;

    public HUD HUD;
	public EarthController Earth;

	internal void EndGame()
	{
		HUD.OnEndGame();
		Debug.Log("End game");
	}
}
