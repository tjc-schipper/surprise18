using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatAnimations : MonoBehaviour
{

	[SerializeField]
	float minHeight;
	[SerializeField]
	float maxHeight;

	private Vector3 basePosition;
	private int loadsRemaining = 4;


	void Start()
	{
		SetLoad(1f);
		this.loadsRemaining = 4;
		StartBobbing();
	}

	public void PresentStackRemoved()
	{
		this.loadsRemaining--;
		float load = (float)this.loadsRemaining / 4f;
		SetLoad(load);
	}

	public void SetLoad(float percentage)
	{
		float y = Mathf.Lerp(this.maxHeight, this.minHeight, percentage);
		this.transform.DOMoveY(y, 1f)
			.SetEase(Ease.OutBack);
	}


	private void StartBobbing()
	{
		this.transform.localRotation *= Quaternion.Euler(-3f, 0f, -1f);

		this.transform.DOBlendableLocalRotateBy(new Vector3(6f, 0f, 0f), 3f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
		this.transform.DOBlendableLocalRotateBy(new Vector3(0f, 0f, 2f), 4f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
	}
}
