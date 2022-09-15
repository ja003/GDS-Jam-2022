using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
	protected Game game => Game.Instance;
	protected Director director => Director.Instance;

	protected const float DIST_OFFSET = 0.1f;

	protected void DoInTime(Action pAction, float pTime)
	{
		StartCoroutine(DoInTimeC(pAction, pTime));
	}

	private IEnumerator DoInTimeC(Action pAction, float pTime)
	{
		yield return new WaitForSeconds(pTime);
		pAction.Invoke();
	}

	protected float GetDistanceTo(Transform pTransform)
	{
		return GetDistanceTo(pTransform.position);
	}

	protected float GetDistanceTo(Vector3 pPosition)
	{
		return Vector3.Distance(transform.position, pPosition);
	}

	/// <summary>
	/// Instantly sets alpha to image color property
	/// </summary>
	protected void SetAlpha(float pAlpha)
	{
		image.enabled = true;
		image.color = new Color(image.color.r, image.color.g, image.color.b, pAlpha);
	}

	/// <summary>
	/// Animates alpha value of image over given duration and calls action on complete
	/// </summary>
	protected void SetAlpha(float pValue, float pDuration, Action pOnComplete)
	{
		LeanTween.alpha(image.rectTransform, pValue, pDuration)
					.setIgnoreTimeScale(true)
					.setOnComplete(pOnComplete);
	}

	/// SELF-GETTERS

	private SpriteRenderer _spriteRend;
	protected SpriteRenderer spriteRend
	{
		get
		{
			if(_spriteRend == null)
				_spriteRend = GetComponent<SpriteRenderer>();
			return _spriteRend;
		}
	}

	private SkinnedMeshRenderer _skinMeshRend;
	protected SkinnedMeshRenderer skinMeshRend
	{
		get
		{
			if (_skinMeshRend == null)
				_skinMeshRend = GetComponent<SkinnedMeshRenderer>();
			return _skinMeshRend;
		}
	}

	private Rigidbody2D _rb;
	protected Rigidbody2D rb
	{
		get
		{
			if(_rb == null)
				_rb = GetComponent<Rigidbody2D>();
			return _rb;
		}
	}

	private Animator _animator;
	protected Animator animator
	{
		get
		{
			if(_animator == null)
				_animator = GetComponent<Animator>();
			return _animator;
		}
	}


	private BoxCollider2D _boxCollider2D;
	protected BoxCollider2D boxCollider2D
	{
		get
		{
			if(_boxCollider2D == null)
				_boxCollider2D = GetComponent<BoxCollider2D>();
			return _boxCollider2D;
		}
	}

	private Image _image;
	protected Image image
	{
		get
		{
			if(_image == null)
				_image = GetComponent<Image>();
			return _image;
		}
	}

	private Button _button;
	protected Button button
	{
		get
		{
			if(_button == null)
				_button = GetComponent<Button>();
			return _button;
		}
	}

	private RectTransform _rectTransform;
	protected RectTransform rectTransform
	{
		get
		{
			if(_rectTransform == null)
				_rectTransform = GetComponent<RectTransform>();
			return _rectTransform;
		}
	}
}
