using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFinalPresent : MonoBehaviour
{

	[SerializeField]
	AudioClip clip_Hurray;

	[SerializeField]
	AudioClip clip_Fanfare;



	public void PlayAudio()
	{
		AudioSource.PlayClipAtPoint(this.clip_Fanfare, Camera.main.transform.position, 1f);
		DG.Tweening.DOVirtual.DelayedCall(1.5f, () =>
		{
			AudioSource.PlayClipAtPoint(this.clip_Hurray, Camera.main.transform.position, 1f);
		});
	}
}
