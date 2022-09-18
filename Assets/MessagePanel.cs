using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EMessageType
{
	Detected,
	ProbeDestroyed,
	GameOver,
	ProbeEscaped,
	ScientistAbducted,
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
	public TMP_Text TextBox; 

	public float HiddenY;
	public float AnimationSpeed;

	public List<Message> Messages;

	private RectTransform tr;
	private Message message;

	private bool active;

	void Start()
	{
		active = false;
		ChangeY(HiddenY);
	}

	public void ShowMessage(EMessageType pType)
	{
		if (active)
			return;

		if (pType == EMessageType.Detected && Random.Range(0f, 100f) < 70f)
			return;

		if (pType == EMessageType.ProbeDestroyed && Random.Range(0f, 100f) < 70f)
			return;

		if (pType == EMessageType.ProbeEscaped && Random.Range(0f, 100f) < 50f)
			return;

		if (pType == EMessageType.ScientistAbducted && Random.Range(0f, 100f) < 20f)
			return;

		List<Message> possibleMessages = Messages.FindAll((x) => x.MessageType == pType);
		message = possibleMessages[Random.Range((int)0, possibleMessages.Count - 1)];

		TextBox.text = message.MessageText;

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
		active = true;

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

		active = false;
	}
}
