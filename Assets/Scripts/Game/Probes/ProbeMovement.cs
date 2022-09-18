using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeMovement : MonoBehaviour
{
    [SerializeField] private float gravityMagnitude;

	[SerializeField] private AnimationCurve takeoffAngleCurve;
	[SerializeField] private AnimationCurve takeoffForceCurve;

	public float UpwardForceMultiplier;
	public float OrbitForceMultiplier;
	public float TakeoffDuration;
	public float SetoffToOuterSpaceDuration;
	public Vector3 CenterOfUniverse => Vector3.zero;

	public ParticleSystem SmokeEffect;
	public ParticleSystem FlameEffect;

	private Rigidbody rb;
	private Transform tr;

	public float ScaleAnimationDuration;
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
		if (timeSinceSpawn <= ScaleAnimationDuration) {
			float scale = (timeSinceSpawn / ScaleAnimationDuration) * targetScale;
			tr.localScale = new Vector3(scale, scale, scale);
		}

		// take off movement
		if (timeSinceSpawn <= TakeoffDuration) 
		{
			Vector3 awayFromEarth = tr.position.normalized * UpwardForceMultiplier;
			Vector3 orbitDirection = (new Vector3(-awayFromEarth.y, awayFromEarth.x)).normalized * OrbitForceMultiplier;
			Vector3 direction = Vector3.Slerp(awayFromEarth, orbitDirection, takeoffAngleCurve.Evaluate(timeSinceSpawn / TakeoffDuration));
			Vector3 force = direction * takeoffForceCurve.Evaluate(timeSinceSpawn / TakeoffDuration);
			rb.AddForce(force);

			// rotation
			transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
		}

		// particles shutdown
		if (timeSinceSpawn > TakeoffDuration && timeSinceSpawn <= SetoffToOuterSpaceDuration)
		{
			SmokeEffect.Stop();
			FlameEffect.Stop();
		}

		if(timeSinceSpawn > SetoffToOuterSpaceDuration)
        {
			SmokeEffect.TryPlay();
			FlameEffect.TryPlay();

			var awayFromEarth =  (tr.position - CenterOfUniverse).normalized;
			var force = awayFromEarth * UpwardForceMultiplier;
			rb.AddForce(force);

        }

		// gravity
		Vector3 gravity = -tr.position.normalized * gravityMagnitude;
		rb.AddForce(gravity);
	}
}
