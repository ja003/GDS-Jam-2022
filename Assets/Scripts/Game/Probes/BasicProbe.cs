using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProbe : GameBehaviour, IDamagable
{
    [SerializeField] private float gravityMagnitude;

	[SerializeField] private AnimationCurve takeoffAngleCurve;
	[SerializeField] private AnimationCurve takeoffForceCurve;
	[SerializeField] private float maxForce;
	[SerializeField] private float takeoffDuration;

	[SerializeField] private float scaleAnimationDuration;

	[SerializeField] private RewardObject RewardPrefab;
	[SerializeField] private int MinRewardXP = 1;
	[SerializeField] private int MaxRewardXP = 5;
    [SerializeField] private float RewardAmmoChance = 0.5f;
    [SerializeField] private int MinRewardAmmo = 5;
    [SerializeField] private int MaxRewardAmmo = 15;

    private Rigidbody rb;
	private Transform tr;

	private float targetScale;

	private float timeSinceSpawn;

	public int Health = 1;

	public void OnHit(int pDamage)
	{
		Health -= pDamage;
		//Debug.Log($"Hit. {Health} left");

		if(Health <= 0)
		{
			if(Random.Range(0,1f) > RewardAmmoChance)
			{
                RewardObject rewardAmmo = Instantiate(RewardPrefab, game.ProbesHolder);
				float rangeOffsetX = Random.Range(0, 1);
				float rangeOffsetY = Random.Range(0, 1);
				rewardAmmo.transform.position = transform.position + new Vector3(rangeOffsetX, rangeOffsetY, 0);

                rewardAmmo.Type = EReward.Ammo;
				rewardAmmo.Amount = Random.Range(MinRewardXP, MaxRewardXP) + Health;
            }

            RewardObject rewardXP = Instantiate(RewardPrefab, game.ProbesHolder);
            float rangeOffsetX2 = Random.Range(0, 1);
            float rangeOffsetY2 = Random.Range(0, 1);
            rewardXP.transform.position = transform.position + new Vector3(-rangeOffsetX2, rangeOffsetY2, 0) * Random.Range(2, 4f);

            rewardXP.Type = EReward.XP;
            rewardXP.Amount = Random.Range(MinRewardAmmo, MaxRewardAmmo) + Health * 3;

            Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
		tr = GetComponent<Transform>();
		targetScale = tr.localScale.x;
		timeSinceSpawn = 0f;
		tr.localScale = new Vector3(0f, 0f, 0f);
	}

    // Update is called once per frame
    void Update()
    {
		timeSinceSpawn += Time.deltaTime;

		// spawn groving animation
		if (timeSinceSpawn <= scaleAnimationDuration) {
			float scale = (timeSinceSpawn / scaleAnimationDuration) * targetScale;
			tr.localScale = new Vector3(scale, scale, scale);
		}

		// take off movement
		if (timeSinceSpawn <= takeoffDuration) 
		{
			Vector3 awayFromEarth = tr.position.normalized;
			Vector3 orbitDirection = new Vector3(-awayFromEarth.y, awayFromEarth.x);
			Vector3 direction = Vector3.Slerp(awayFromEarth, orbitDirection, takeoffAngleCurve.Evaluate(timeSinceSpawn / takeoffDuration));
			Vector3 force = direction * takeoffForceCurve.Evaluate(timeSinceSpawn / takeoffDuration) * maxForce;
			rb.AddForce(force);
		}

		// gravity
		Vector3 gravity = -tr.position.normalized * gravityMagnitude;
		rb.AddForce(gravity);
	}
}
