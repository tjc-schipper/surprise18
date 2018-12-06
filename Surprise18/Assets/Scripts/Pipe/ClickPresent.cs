using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickPresent : MonoBehaviour {


	[SerializeField] float duration = 2.0f;

	public void PickupPresent()
	{
		this.transform.DOScale(6f, this.duration)
			.SetEase(Ease.InOutQuad)
			.OnComplete(() =>
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(3);
			});

		Camera.main.GetComponent<ScreenTransition>().DoTransition(0f, this.duration, this.duration / 2f);
	}
}
