using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToRotate : MonoBehaviour
{

	public delegate void RotateDragEvent(DragToRotate sender, float angle);
	public event RotateDragEvent StopDragging;


	[SerializeField]
	AudioSource audio_Rotate;


	[SerializeField]
	Clickable clickable;

	[SerializeField]
	Transform targetOrientation;

	[SerializeField]
	Transform model;

	[SerializeField]
	float sensitivity = 1f;


	private bool dragging = false;
	private float angle = 0f;
	private float lastY;


	void OnEnable()
	{
		this.clickable.MouseDown += Clickable_MouseDown;
		this.dragging = false;
		ApplyRandomStartRotation();
	}

	void OnDisable()
	{
		this.clickable.MouseDown -= Clickable_MouseDown;
		this.dragging = false;
	}


	void LateUpdate()
	{
		if (this.dragging)
		{
			// Check if we shouldn't stop dragging yet!
			if (Input.GetMouseButton(0) == false)
			{
				Clickable_MouseUp(null, null);
				return;
			}

			float y = Input.mousePosition.y;
			float dy = y - this.lastY;
			float direction = Mathf.Sign(Vector3.Dot(Camera.main.transform.forward, -this.targetOrientation.right));    // If looking from backside, invert rotation!
			this.lastY = y;

			this.audio_Rotate.volume = Mathf.Lerp(0f, 1f, Mathf.Abs(dy) / 10f);

			this.angle += dy * sensitivity * direction;
			this.model.rotation = Quaternion.Euler(0f, 0f, this.angle) * this.targetOrientation.rotation;
		}
	}


	private void Clickable_MouseDown(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		this.dragging = true;
		this.lastY = Input.mousePosition.y;
		this.angle = Quaternion.Angle(this.model.rotation, this.targetOrientation.rotation);
		this.audio_Rotate.Play();
	}

	private void Clickable_MouseUp(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		this.dragging = false;
		this.audio_Rotate.Stop();
		if (this.StopDragging != null)
			this.StopDragging.Invoke(this, this.angle);
	}

	private void ApplyRandomStartRotation()
	{
		this.angle = Random.Range(90, 270f);
		this.model.rotation = Quaternion.Euler(0f, 0f, this.angle) * this.targetOrientation.rotation;
	}


	public float GetAngleDifference()
	{
		return Quaternion.Angle(this.model.rotation, this.targetOrientation.rotation);
	}

	public void ForceAngle(float angle)
	{
		this.angle = angle;
		this.model.rotation = Quaternion.Euler(0f, 0f, angle) * this.targetOrientation.rotation;
	}

}
