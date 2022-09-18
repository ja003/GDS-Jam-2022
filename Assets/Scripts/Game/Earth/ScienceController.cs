using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScienceController : GameBehaviour
{
	[SerializeField] Scientist ScientistPrefab;
	[SerializeField] Transform ScientistSpawnPlace;

	[SerializeField] float RotationSpeed = 1;
	[SerializeField] int MinXP = 1;

	[SerializeField] int MinCooldown = 5;
	[SerializeField] int MaxCooldown = 15;

	Scientist ActiveScientist;
	bool HasScienceStarted;
	
	private void StartScience()
	{
		GenerateScientist();
		HasScienceStarted = true;
	}

	internal void OnXPChanged(int pXP)
	{
		if(pXP < MinXP || HasScienceStarted)
			return;

		StartScience();
	}

	private void GenerateScientist()
	{
		if(ActiveScientist == null)
			ActiveScientist = Instantiate(ScientistPrefab, ScientistSpawnPlace);

		DoInTime(GenerateScientist, Random.Range(MinCooldown, MaxCooldown));
	}
}
