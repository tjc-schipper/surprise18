using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PoemScreen : MonoBehaviour
{

	[SerializeField]
	float transitionDuration;

	[SerializeField]
	Text poemText;

	[SerializeField]
	Image backgroundFill;


	void Awake()
	{
		this.poemText.enabled = false;
		this.backgroundFill.enabled = false;
	}

	void OnEnable()
	{
		this.backgroundFill.color = new Color(0f, 0f, 0f, 0f);
		this.poemText.color = new Color(1f, 1f, 1f, 0f);

		this.poemText.enabled = true;
		this.backgroundFill.enabled = true;

		DOVirtual.Float(0f, 1f, this.transitionDuration, (float p) =>
		{
			this.backgroundFill.color = new Color(0f, 0f, 0f, 0.75f * p);
			this.poemText.color = new Color(1f, 1f, 1f, p);
		});
	}

	void OnDisable()
	{
		DOVirtual.Float(1f, 0f, this.transitionDuration, (float p) =>
		{
			this.backgroundFill.color = new Color(0f, 0f, 0f, 0.75f * p);
			this.poemText.color = new Color(1f, 1f, 1f, p);
		})
		.OnComplete(() =>
		{
			this.poemText.enabled = false;
			this.backgroundFill.enabled = false;
		});
	}


	void LateUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.enabled = false;
		}
	}
}
