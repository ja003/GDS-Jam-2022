using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static void StopAndReset(this AudioSource self)
    {
		self.Stop();
		self.time = 0f;
    }
    internal static void DebugDrawCross(Vector2 pPosition, Color pColor, float pDuration = -1)
	{
		if(pDuration <= 0)
			pDuration = Time.deltaTime;

		const float length = 0.05f;
		//const float duration = 0.5f;
		Debug.DrawLine(pPosition + Vector2.left * length, pPosition + Vector2.right * length, pColor, pDuration);
		Debug.DrawLine(pPosition + Vector2.down * length, pPosition + Vector2.up * length, pColor, pDuration);
	}

	public static float GetDistanceFromBox(Vector2 pPoint, BoxCollider2D pBox)
	{
		float dist = Vector3.Distance(pPoint, pBox.bounds.ClosestPoint(pPoint));
		return dist;
	}

	public static float GetDistanceFromLine(Vector2 pPoint, Vector2 pLineStart, Vector2 pLineDirection)
	{
		Vector2 nearestPoint = FindNearestPointOnLine(pLineStart, pLineDirection, pPoint);
		float dist = Vector2.Distance(pPoint, nearestPoint);
		return dist;
	}


	private static Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 direction, Vector2 point)
	{
		direction.Normalize();
		Vector2 lhs = point - origin;

		float dotP = Vector2.Dot(lhs, direction);
		return origin + direction * dotP;
	}

	public static Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
	{
		//Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		Vector2 movePos;

		//Convert the screenpoint to ui rectangle local point
		RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
		//Convert the local point to world point
		return parentCanvas.transform.TransformPoint(movePos);
	}

	public static IEnumerable<YieldInstruction> MakeInterpolation(float duration, System.Action<float> toDo)
	{
		double startTime = Time.timeAsDouble, endTime = startTime + duration;
		for (double currentTime; (currentTime = Time.timeAsDouble) < endTime;)
		{
			var t = (float)(currentTime - startTime) / duration;
			toDo(t);
			yield return null;
		}
		toDo(1f);
	}


	public static void StartIndependentCoroutine(System.Func<GameObject, IEnumerator> coroutine)
    {
		var obj = new GameObject();
		var component = obj.AddComponent<EmptyComponent>();
		component.StartCoroutine(coroutine(obj));

    }
	public static void PlayOneShotIndependently(AudioClip clip)
    {
		StartIndependentCoroutine(impl);
		IEnumerator impl(GameObject o)
        {
			var player = o.AddComponent<AudioSource>();

			player.PlayOneShot(clip);

			yield return new WaitUntil(() => !player.isPlaying);
			Object.Destroy(o);
        }
    }

	public static void TryPlay(this ParticleSystem self)
    {
		if (!self.isPlaying) self.Play();
    }
}

public class EmptyComponent : MonoBehaviour { }
