using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
	public float Duration;
	public float Cooldown;
	public float CooldownVariance;
	public float UpwardForceMultiplier;
	public float OrbitForceMultiplier;
	public float TakeoffDuration;
	public float TakeoffVariance;
	public bool UseSmallProbes;
	public bool UseBigProbes;
}

public class ProbeSpawner : GameBehaviour
{
	public float Radius;

	public GameObject SmallProbePrefab;
	public GameObject BigProbePrefab;

	public List<Wave> Waves;

	private float timeInWave;
	private float cooldownRemaining;
	private int currentWaveNumber;
	private Wave currentWave;

	// Start is called before the first frame update
	void Start()
    {
        timeInWave = 0f;
		cooldownRemaining = 0.5f;
		currentWaveNumber = 0;
		currentWave = Waves[currentWaveNumber];
		currentWave.Cooldown += Random.Range(-currentWave.CooldownVariance, currentWave.CooldownVariance);
	}

    // Update is called once per frame
    void Update()
    {
		if(!game.IsInGame)
			return;
	
		timeInWave += Time.deltaTime;
		cooldownRemaining -= Time.deltaTime;

		if (timeInWave < currentWave.Duration)
		{
			if (cooldownRemaining <= 0f)
			{
				float randomAngle = Random.Range(0f, 2 * Mathf.PI);
				Vector3 direction = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
				Vector3 spawnPoint = direction * Radius;

				bool spawnBig = !currentWave.UseSmallProbes || (currentWave.UseSmallProbes && currentWave.UseBigProbes && Random.Range(0f, 1f) < 0.5f);

				var newInst = Instantiate(spawnBig ? BigProbePrefab : SmallProbePrefab, spawnPoint, Quaternion.LookRotation(direction, Vector3.up), game.ProbesHolder);
				newInst.gameObject.SetActive(true);

				ProbeMovement probe = newInst.GetComponent<ProbeMovement>();
				probe.TakeoffDuration = currentWave.TakeoffDuration + Variance(currentWave.TakeoffVariance);
				probe.UpwardForceMultiplier = currentWave.UpwardForceMultiplier + Variance(currentWave.TakeoffVariance);
				probe.OrbitForceMultiplier = currentWave.OrbitForceMultiplier + Variance(currentWave.TakeoffVariance);

				cooldownRemaining = currentWave.Cooldown + Variance(currentWave.CooldownVariance);
			}
		}
		else
		{
			timeInWave = 0f;
			currentWaveNumber += Waves.Count > currentWaveNumber + 1 ? 1 : 0;
			currentWave = Waves[currentWaveNumber];
			currentWave.Cooldown += Variance(currentWave.CooldownVariance);
		}
    }

	private float Variance(float pRange)
	{
		return Random.Range(-pRange, pRange);
	}
}
