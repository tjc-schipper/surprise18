using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFlapAfterPresents : MonoBehaviour
{

	[SerializeField]
	AudioClip clip_Squeak;

	[SerializeField]
	AudioClip clip_Bang;


	[SerializeField]
	PresentStack[] presents;

	[SerializeField]
	private OneTimeClickable screwClickable;

	[SerializeField]
	BoatAnimations boatAnimations;

	private Animator animator;
	private int presentCounter;
	private int numPresents;

	// Use this for initialization
	void Start()
	{
		this.animator = GetComponent<Animator>();
		this.presentCounter = 0;
		this.numPresents = this.presents.Length;

		foreach (PresentStack stack in this.presents)
		{
			stack.OnExploded += HandlePresent;
		}

		this.screwClickable.enabled = false;
	}


	void HandlePresent()
	{
		this.boatAnimations.PresentStackRemoved();
		this.presentCounter++;
		if (this.presentCounter == this.numPresents)
		{
			Open();
		}
	}

	void Open()
	{
		this.animator.SetBool("b_Open", true);
		AudioSource.PlayClipAtPoint(this.clip_Squeak, Camera.main.transform.position, 1f);
		DG.Tweening.DOVirtual.DelayedCall(2.5f, () =>
		{
			AudioSource.PlayClipAtPoint(this.clip_Bang, Camera.main.transform.position, 1f);
			this.screwClickable.enabled = true;
		});
	}

}
