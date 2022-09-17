using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Probe owner;

	public MeshRenderer ConeMesh;
	public Material RedMaterial;
	public Material YellowMaterial;

	public bool IsTriggered = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			IsTriggered = true;
			ConeMesh.material = RedMaterial;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.DrawLine(transform.position, other.gameObject.transform.position, Color.red);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			IsTriggered = false;
			ConeMesh.material = YellowMaterial;
		}
	}
}
