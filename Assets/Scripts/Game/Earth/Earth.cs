using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
	[SerializeField] float RotationSpeed = 1;

	private void FixedUpdate()
	{
		transform.Rotate(Vector3.up, -RotationSpeed);
	}
}
