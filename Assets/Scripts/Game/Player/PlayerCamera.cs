using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 0.1f;

	private void Awake()
	{
		if (target == null)
		{
            target = FindObjectOfType<Player>().transform;
		}
	}
	private void Start()
    {
        // You can also specify your own offset from inspector as it is public variable
		Vector3 properPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
		transform.position = properPosition;
		offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
        transform.LookAt(target);
    }
}