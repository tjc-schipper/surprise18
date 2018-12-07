using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class JiggleDoor : MonoBehaviour
{

	[SerializeField]
	AudioClip clip_Jiggle;

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
		AudioSource.PlayClipAtPoint(this.clip_Jiggle, Camera.main.transform.position, 1f);
		this.animator.SetTrigger("t_Jiggle");
	}

}
