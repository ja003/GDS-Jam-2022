using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMessageType
{
	Detected,
	ProbeDestroyed,
	GameOver,
	ProbeEscaped,
	TutorialProbeEscape
}

[System.Serializable]
public struct Message
{
	public EMessageType MessageType;
	public string MessageText;
	public float Duration;
}

public class MessagePanel : MonoBehaviour
{
	public float HiddenY;
	public float AnimationSpeed;

	public List<Message> Messages;

	private RectTransform tr;
	private Message message;

	private float timeAtAnimStart = -1f;

	void Start()
	{
		ChangeY(HiddenY);
	}

	public void ShowMessage(EMessageType pType)
	{
		List<Message> possibleMessages = Messages.FindAll((x) => x.MessageType == pType);
		message = possibleMessages[Random.Range((int)0, possibleMessages.Count - 1)];

		StartCoroutine("Show");
	}

	private void ChangeY(float pValue)
	{
		Vector3 position = GetComponent<RectTransform>().anchoredPosition3D;
		position.y = pValue;
		GetComponent<RectTransform>().anchoredPosition3D = position;
	}

	IEnumerator Show()
	{
		for (float y = HiddenY; y >= 0; y -= AnimationSpeed)
		{
			ChangeY(y);
			yield return null;
		}

		StartCoroutine("Hide");
	}

	IEnumerator Hide()
	{
		yield return new WaitForSeconds(message.Duration);
		
		for (float y = 0; y <= HiddenY; y += AnimationSpeed)
		{
			ChangeY(y);
			yield return null;
		}
	}
}
