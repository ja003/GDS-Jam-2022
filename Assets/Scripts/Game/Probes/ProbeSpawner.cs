using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeSpawner : GameBehaviour
{
	[SerializeField] private float cooldown;
	[SerializeField] private float radius;

	[SerializeField] private BasicProbe probePrefab;

	float cooldownRemainig;

	// Start is called before the first frame update
    void Start()
    {
        cooldownRemainig = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRemainig -= Time.deltaTime;

		if (cooldownRemainig <= 0f)
		{
			float randomAngle = Random.Range(0f, 2 * Mathf.PI);
			Vector3 direction = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
			Vector3 spawnPoint = direction * radius;

			var newInst = Instantiate(probePrefab, spawnPoint, Quaternion.identity);
			newInst.transform.parent = game.ProbesHolder;
            newInst.gameObject.SetActive(true);

            cooldownRemainig = cooldown;
		}
    }
}
