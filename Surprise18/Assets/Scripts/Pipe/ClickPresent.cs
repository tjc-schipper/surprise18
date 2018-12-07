using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickPresent : MonoBehaviour {


	[SerializeField]
	AudioClip clip_Secret;

	[SerializeField] float duration = 2.0f;

	public void PickupPresent()
	{
		AudioSource.PlayClipAtPoint(this.clip_Secret, Camera.main.transform.position, 1f);

		this.transform.DOScale(6f, this.duration)
			.SetEase(Ease.InOutQuad)
			.OnComplete(() =>
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(3);
			});

		Camera.main.GetComponent<ScreenTransition>().DoTransition(0f, this.duration, this.duration / 2f);
	}
}
