using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : GameBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] float speed = 1;
	[SerializeField] ForceMode mode = ForceMode.Force;
	[SerializeField] float maxVelocity = 8f;

	[SerializeField] EntryZone[] EntryZones;

    [System.Serializable]
	public class EntryZone
    {
		public Vector3 Center;

		public float Radius, MaxEscapeFromBounds;

		public NoEntryMode Mode = NoEntryMode.StayOutside;
		public enum NoEntryMode { StayInside = -1, StayOutside = 1 }
	}

	void FixedUpdate()
	{
		if(!game.IsInGame)
			return;

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

		foreach(var n in EntryZones)
			forceToAdd = ModifyByNoEntryZone(forceToAdd, n);

		forceToAdd *= speed * Time.deltaTime;
		rb.AddForce(forceToAdd);
	}


	private Vector3 ModifyByNoEntryZone(Vector3 currentForceToAdd, EntryZone n)
	{
		var m = (int)n.Mode;
		var distanceFromCenter = Vector3.Distance(rb.position, n.Center);
		if (distanceFromCenter.CompareTo(n.Radius) == -m)
		{
			var avayFromCenter = ((rb.position - n.Center)*m).normalized;
			var lerpFactor = Mathf.Min(n.MaxEscapeFromBounds, (n.Radius - distanceFromCenter)*m) / n.MaxEscapeFromBounds;
			currentForceToAdd = Vector3.Lerp(currentForceToAdd, avayFromCenter, lerpFactor).normalized;
		}
		return currentForceToAdd;
	}


	private void OnDrawGizmosSelected()
	{
		foreach(var n in EntryZones)
		{
			Gizmos.color = n.Mode switch { EntryZone.NoEntryMode.StayInside => Color.green, EntryZone.NoEntryMode.StayOutside => Color.yellow };
			Gizmos.DrawWireSphere(n.Center, n.Radius);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(n.Center, n.Radius - n.MaxEscapeFromBounds);
		}

	}
}