using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PathDraggable))]
public class EndBoatScene : MonoBehaviour
{

	[SerializeField]
	float triggerPercentage = 0.85f;

	[SerializeField]
	Transform endLocation;

	[SerializeField]
	float duration = 2f;

	private PathDraggable draggablePipe;


	void Start()
	{
		this.draggablePipe = GetComponent<PathDraggable>();
		this.draggablePipe.StopDragging += DraggablePipe_AbortDragging;
	}


	private void DraggablePipe_AbortDragging(PathDraggable sender, float percentage)
	{
		if (percentage >= this.triggerPercentage)
		{
			this.draggablePipe.StopDragging -= DraggablePipe_AbortDragging;
			this.draggablePipe.GetComponent<Clickable>().enabled = false;
			this.draggablePipe.enabled = false;
			End();
		}
	}


	private void End()
	{
		// Move pipe to endLocation
		GameObject go = this.draggablePipe.gameObject;
		go.transform.DOMove(endLocation.position, this.duration)
			.SetEase(Ease.InOutQuart)
			.OnComplete(() =>
			{
				// TODO: Transition to next scene!
				Debug.Log("LOAD NEW SCENE!");
				UnityEngine.SceneManagement.SceneManager.LoadScene(2);
			});
		go.transform.DORotateQuaternion(this.endLocation.rotation, this.duration)
			.SetEase(Ease.InOutQuad);
		go.transform.DOScale(7.5f, this.duration)
			.SetDelay(this.duration / 2f);

		// Trigger a screen transition
		Camera.main.GetComponent<ScreenTransition>().DoTransition(0f, this.duration, this.duration / 2f);
	}
}
