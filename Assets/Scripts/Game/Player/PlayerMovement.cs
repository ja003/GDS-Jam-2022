using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] float speed = 1;
	[SerializeField] ForceMode mode = ForceMode.Force;
	[SerializeField] float maxVelocity = 8f;

	[SerializeField] float maxDistanceFromCenter = 25f, maxEscapeFromBounds = 2f;

	Vector3 playgroundCentre => Vector3.zero;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//Debug.Log($"velocity: {rb.velocity.magnitude}");
		if(rb.velocity.magnitude > maxVelocity) rb.velocity = rb.velocity.normalized * maxVelocity;

		var forceToAdd = Vector3.zero;
		if(Input.GetKey(KeyCode.W))
		{
			forceToAdd += (Vector3.up);
		}
		if(Input.GetKey(KeyCode.A))
		{
			forceToAdd += (Vector3.left);
		}
		if(Input.GetKey(KeyCode.S))
		{
			forceToAdd += (Vector3.down);
		}
		if(Input.GetKey(KeyCode.D))
		{
			forceToAdd += (Vector3.right);
		}
		forceToAdd = forceToAdd.normalized;

		var distanceFromCenter = Vector3.Distance(rb.position, playgroundCentre);
		if(distanceFromCenter > maxDistanceFromCenter)
		{
			var toCenter = (playgroundCentre - rb.position).normalized;
			var lerpFactor = Mathf.Min(maxEscapeFromBounds, distanceFromCenter - maxDistanceFromCenter) / maxEscapeFromBounds;
			forceToAdd = Vector3.Lerp(forceToAdd, toCenter, lerpFactor).normalized;
		}
		forceToAdd *= speed * Time.deltaTime;
		rb.AddForce(forceToAdd);
		//rb.velocity = forceToAdd;
	}


	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(playgroundCentre, maxDistanceFromCenter);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(playgroundCentre, maxDistanceFromCenter + maxEscapeFromBounds);
	}
}