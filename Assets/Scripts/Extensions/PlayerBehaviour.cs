using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	[SerializeField] private Player _player;
	protected Player Player
	{
		get
		{
			if(_player == null)
				_player = GetComponent<Player>();
			return _player;
		}
	}
}
