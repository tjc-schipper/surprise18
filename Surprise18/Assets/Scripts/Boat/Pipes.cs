using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pipes : MonoBehaviour
{

	[SerializeField]
	AudioClip clip_Pipefall;

	[SerializeField]
	AudioClip clip_PipeJiggle;

	[SerializeField]
	private PathDraggable draggablePipe;

	[SerializeField]
	private GameObject placeholderPipe;

	private Animator animator;

	// Use this for initialization
	void Start()
	{
		this.animator = GetComponent<Animator>();
		this.draggablePipe.gameObject.SetActive(false);
	}


	public void DoUnlock()
	{
		this.animator.SetBool("b_Loose", true);
		AudioSource.PlayClipAtPoint(this.clip_Pipefall, Camera.main.transform.position, 1f);
		
		DG.Tweening.DOVirtual.DelayedCall(1.55f, () =>
		{
			this.draggablePipe.gameObject.SetActive(true);
			this.draggablePipe.transform.position = this.placeholderPipe.transform.position;
			this.placeholderPipe.SetActive(false);
			Debug.Log("UNLOCKED PIPES!");
		});
	}

	public void DoJiggle()
	{
		AudioSource.PlayClipAtPoint(this.clip_PipeJiggle, Camera.main.transform.position, 1f);
		this.animator.SetTrigger("t_Jiggle");
	}
}
