using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UndoScrew : MonoBehaviour
{

	[SerializeField]
	AudioClip clip_Screw;

	public UnityEngine.Events.UnityEvent OnScrewUndone;

	private Animator animator;

	void Start()
	{
		this.animator = GetComponent<Animator>();
	}

	public void Undo()
	{
		this.animator.SetBool("b_Undone", true);
		DG.Tweening.DOVirtual.DelayedCall(1f, () =>
		{
			AudioSource.PlayClipAtPoint(this.clip_Screw, Camera.main.transform.position, 1f);
			if (this.OnScrewUndone != null)
				this.OnScrewUndone.Invoke();
		});
	}

}
