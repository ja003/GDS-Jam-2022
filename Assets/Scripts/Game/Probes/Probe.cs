using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : GameBehaviour, IDamagable
{
	[SerializeField] private RewardObject RewardXPPrefab;
	[SerializeField] private int MinRewardXP = 1;
	[SerializeField] private int MaxRewardXP = 5;
	[SerializeField] private RewardObject RewardAmmoPrefab;
	[SerializeField] private float RewardAmmoChance = 0.5f;
	[SerializeField] private int MinRewardAmmo = 5;
	[SerializeField] private int MaxRewardAmmo = 15;
	[SerializeField] private float DetectionAddedOnSuccess = 20f;
	[SerializeField] AudioClip DieSound, SuccessSound;
	[SerializeField] private float DieAnimationDuration;
	[SerializeField] DamagedAnimationInfo DamageAnim;

	public float SuccessTargetDistance = 25f;
	public Vector3 CenterOfUniverse => Vector3.zero;


    [System.Serializable]
	public struct DamagedAnimationInfo
    {
		public Material BlinkMaterial;
		public MeshRenderer Blinker;
		public int BlinksCount;
		public float BlinkInterval, BlinkDuration;
    }


	public int TotalHealth = 1;
	private int HealthLeft = 1;

	public List<Detector> detectors;

	private Game game;
	private PlayerStats playerStats;

	// Start is called before the first frame update
	void Start()
	{
		HealthLeft = TotalHealth;
		game = FindObjectOfType<Game>();
		playerStats = FindObjectOfType<PlayerStats>();

		DoInTime(() => { canCollideEarth = true; }, 2);
	}

	void Update()
	{
		foreach (Detector detector in detectors)
		{
			if (detector.IsTriggered)
			{
				playerStats.AddDetection(Time.deltaTime);

				game.HUD.ShowMessage(EMessageType.Detected);
			}
		}
		TrySucceedMission();
	}

	void TrySucceedMission()
    {
		if(Vector3.Distance(transform.position, CenterOfUniverse) > SuccessTargetDistance)
        {
			playerStats.AddDetection(DetectionAddedOnSuccess);

			game.HUD.ShowMessage(EMessageType.ProbeEscaped);

			Utils.PlayOneShotIndependently(SuccessSound);
			Destroy(gameObject);
        }
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (!canCollideEarth)
			return;

		Debug.Log("OnCollisionEnter " + collision.gameObject.name);
		Earth earth = collision.gameObject.GetComponent<Earth>();

		if (earth != null)
			Die(true);
	}

	bool canCollideEarth;
    public void OnHit(int pDamage)
	{
		HealthLeft -= pDamage;
		//Debug.Log($"Hit. {Health} left");

		if (HealthLeft <= 0)
		{
			Die();
		}
		PlayDamagedAnimation(DamageAnim);
	}


	bool damagedAnimationPlaying = false;
	void PlayDamagedAnimation(DamagedAnimationInfo i)
    {
		if (damagedAnimationPlaying) return;

		damagedAnimationPlaying = true;
		var originalMaterials = i.Blinker.materials;
		StartCoroutine(impl());
		IEnumerator impl()
        {
			for(int t = 0; t < i.BlinksCount; ++t)
            {
				i.Blinker.materials = new[] { i.BlinkMaterial };
				yield return new WaitForSeconds(i.BlinkDuration);
				i.Blinker.materials = originalMaterials;
				if (t < i.BlinksCount - 1)
					yield return new WaitForSeconds(i.BlinkInterval);
            }
			damagedAnimationPlaying = false;
        }
    }

	bool isDying = false;
	void Die(bool pNoReward = false)
	{
		if (isDying) return;
		isDying = true;
		Utils.StartIndependentCoroutine(impl);

		IEnumerator impl(GameObject runner)
        {
			var audioPlayer = runner.AddComponent<AudioSource>();
			audioPlayer.PlayOneShot(DieSound);
			var originalScale = transform.localScale;
			foreach (var y in Utils.MakeInterpolation(DieAnimationDuration, t =>
			{
				t = 1 - t;
				t = t * t * t;
				transform.localScale = originalScale * t;

			})) yield return y;

            if (!pNoReward)
            {
                //reward Ammo
                if (Random.Range(0, 1f) > RewardAmmoChance)
                {
                    RewardObject rewardAmmo = Instantiate(RewardAmmoPrefab, game.RewardsHolder);
                    float rangeOffsetX = Random.Range(0, 1);
                    float rangeOffsetY = Random.Range(0, 1);
                    rewardAmmo.transform.position = transform.position + new Vector3(rangeOffsetX, rangeOffsetY, 0);

                    rewardAmmo.Type = EReward.Ammo;
                    rewardAmmo.Amount = Random.Range(MinRewardXP, MaxRewardXP) + TotalHealth;
                    rewardAmmo.OnSpawn();
                }

                //reward XP
                RewardObject rewardXP = Instantiate(RewardXPPrefab, game.RewardsHolder);
                float rangeOffsetX2 = Random.Range(0, 1);
                float rangeOffsetY2 = Random.Range(0, 1);
                rewardXP.transform.position = transform.position + new Vector3(-rangeOffsetX2, rangeOffsetY2, 0) * Random.Range(2, 4f);

                rewardXP.Type = EReward.XP;
                rewardXP.Amount = Random.Range(MinRewardAmmo, MaxRewardAmmo) + TotalHealth * 3;
                rewardXP.OnSpawn();
            }
            Destroy(gameObject);

			// message
			game.HUD.ShowMessage(EMessageType.ProbeDestroyed);

			yield return new WaitUntil(() => !audioPlayer.isPlaying);
			Destroy(runner);
		}

	}
}
