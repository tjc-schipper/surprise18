using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFlapAfterPresents : MonoBehaviour
{

	[SerializeField]
	PresentStack[] presents;

	[SerializeField]
	private OneTimeClickable screwClickable;

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
		this.presentCounter++;
		if (this.presentCounter == this.numPresents)
		{
			Open();
		}
	}

	void Open()
	{
		this.animator.SetBool("b_Open", true);
		DG.Tweening.DOVirtual.DelayedCall(5f, () =>
		{
			this.screwClickable.enabled = true;
		});
	}

}
