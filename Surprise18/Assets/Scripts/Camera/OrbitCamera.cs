using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{

	[SerializeField]
	private Clickable clickable;

	[SerializeField]
	private Transform pivotPoint;

	[SerializeField]
	private Vector2 sensitivity = new Vector2(1f, 1f);


	private bool dragging = false;
	private Vector2 lastMousePos;


	void OnEnable()
	{
		if (this.clickable != null)
		{
			this.clickable.MouseDown += Clickable_MouseDown;
			this.clickable.MouseUp += Clickable_MouseUp;
		}
	}

	void OnDisable()
	{
		if (this.clickable != null)
		{
			this.clickable.MouseDown -= Clickable_MouseDown;
			this.clickable.MouseUp -= Clickable_MouseUp;
		}
	}


	void LateUpdate()
	{
		if (dragging)
		{
			Vector2 currentMousePos = Input.mousePosition;
			Vector2 deltaPos = currentMousePos - this.lastMousePos;

			this.transform.RotateAround(this.pivotPoint.position, Vector3.up, deltaPos.x * this.sensitivity.x); // Pan around global Y
			this.transform.RotateAround(this.pivotPoint.position, this.pivotPoint.right, -deltaPos.y * this.sensitivity.y);  // Tilt around pivot.x

			//TODO: Clamp tilt so you can't go upside down!

			this.lastMousePos = currentMousePos;
		}
	}


	private void Clickable_MouseDown(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		this.dragging = true;
		this.lastMousePos = e.position;
	}

	private void Clickable_MouseUp(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		this.dragging = false;
	}
}
