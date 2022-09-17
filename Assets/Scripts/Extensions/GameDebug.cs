using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebug : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}


#if UNITY_EDITOR
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			Time.timeScale += 0.5f;
			Debug.Log($"Time.timeScale = {Time.timeScale}");
		}
		if(Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			Time.timeScale -= 0.5f;
			Debug.Log($"Time.timeScale = {Time.timeScale}");
		}
	}
#endif
}
