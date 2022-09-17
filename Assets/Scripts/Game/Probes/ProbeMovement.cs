using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeMovement : MonoBehaviour
{
    [SerializeField] private float gravityMagnitude;

	[SerializeField] private AnimationCurve takeoffAngleCurve;
	[SerializeField] private AnimationCurve takeoffForceCurve;
	[SerializeField] private float maxForce;
	[SerializeField] private float takeoffDuration;

	[SerializeField] private float scaleAnimationDuration;

	private Rigidbody rb;
	private Transform tr;

	private float targetScale;

	private float timeSinceSpawn;

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
