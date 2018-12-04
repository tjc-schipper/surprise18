using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class JiggleDoor : MonoBehaviour
{

	[SerializeField]
	Clickable clickable;

	private Animator animator;


	void Start()
	{
		this.animator = GetComponent<Animator>();
		this.clickable.Click += Clickable_Click;
	}

	private void Clickable_Click(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		this.animator.SetTrigger("t_Jiggle");
	}

}
