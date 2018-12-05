using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzle : MonoBehaviour
{

	[SerializeField]
	DragToRotate[] cylinders;

	[SerializeField]
	float angleThreshold = 5f;

	[SerializeField]
	OpenCap openCap;


	// Use this for initialization
	void Start()
	{
		foreach (DragToRotate cylinder in this.cylinders)
		{
			cylinder.StopDragging += D_StopDragging;
		}
	}


	private void D_StopDragging(DragToRotate sender, float angle)
	{
		bool anyIncorrect = false;
		foreach (DragToRotate cylinder in this.cylinders)
		{
			anyIncorrect = anyIncorrect || (cylinder.GetAngleDifference() > this.angleThreshold);
		}

		if (!anyIncorrect)
		{
			CompletePuzzle();
		}
	}

	private void CompletePuzzle()
	{
		foreach (DragToRotate cylinder in this.cylinders)
		{
			cylinder.ForceAngle(0f);
			cylinder.enabled = false;
		}

		//TODO: Trigger something. Like the buttons coming out the side?
		Debug.Log("WE DONE HERE!");
		this.openCap.DoOpen();
	}
}
