using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoToNextSceneAfterDelay : MonoBehaviour {

	[SerializeField] ScreenTransition transition;
	
	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetMouseButtonDown(0))
		{
			this.enabled = false;
			this.transition.DoTransition(0f, 3f, 2f);

			DOVirtual.DelayedCall(5f, () =>
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(1);
			});
		}
	}
}
