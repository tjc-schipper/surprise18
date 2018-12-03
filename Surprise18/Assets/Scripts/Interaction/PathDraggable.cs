using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable))]
public class PathDraggable : MonoBehaviour
{

	public delegate void PathDragEvent(PathDraggable sender, float percentage);
	public event PathDragEvent OnArrive;
	public event PathDragEvent OnDragPercentage;
	public event PathDragEvent AbortDragging;


	[SerializeField]
	private Transform pathStart;

	[SerializeField]
	private Transform pathEnd;

	[SerializeField]
	private float lerpFactor = 10f;


	private const float ARRIVAL_DISTANCE = 0.05f;
	private Clickable clickable;
	private bool dragging = false;
	private Vector3 closestPoint;


	void Start()
	{
		this.clickable = GetComponent<Clickable>();
		this.clickable.MouseDown += Clickable_MouseDown;
	}

	void LateUpdate()
	{
		if (this.dragging)
		{
			// If no longer mouse down, end drag!
			if (Input.GetMouseButton(0) == false)
			{
				Clickable_MouseUp(null, null);
				return;
			}

			// Project mouse position at same distance as path midpoint
			Vector3 midPoint = Vector3.Lerp(this.pathStart.position, this.pathEnd.position, 0.5f);
			Vector3 viewspaceMousPos = Input.mousePosition;
			viewspaceMousPos.z = Vector3.Distance(Camera.main.transform.position, midPoint);
			Vector3 worldspaceMousePos = Camera.main.ScreenToWorldPoint(viewspaceMousPos);

			// Create plane normal to path that intersects mouse pos to create "plane of perpendiculars"
			Plane pointPlane = new Plane((this.pathStart.position - this.pathEnd.position).normalized, worldspaceMousePos);
			// Project any path point onto plane to get intersection, since it's perpendicular :)
			Vector3 intersection = pointPlane.ClosestPointOnPlane(midPoint);


			// BELOW: Clamp intersection to never fall beyond path start/end!
			Vector3 toPoint = intersection - this.pathStart.position;
			Vector3 toEnd = this.pathEnd.position - this.pathStart.position;
			// Check whether point is not 'before' pathStart
			if (Vector3.Dot(toPoint, toEnd) >= 0)
			{
				// Check whether point is not 'past' pathEnd
				if (Vector3.SqrMagnitude(toPoint) <= Vector3.SqrMagnitude(toEnd))
				{
					this.closestPoint = intersection;
				}
				else
				{
					this.closestPoint = this.pathEnd.position;
				}
			}
			else
			{
				this.closestPoint = this.pathStart.position;
			}

			this.transform.position = Vector3.Lerp(this.transform.position, this.closestPoint, this.lerpFactor * Time.deltaTime);

			if (OnDragPercentage != null)
				OnDragPercentage.Invoke(this, GetPathPercentage(this.pathStart.position, this.pathEnd.position, this.closestPoint));
		}
	}


	private void Clickable_MouseDown(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		this.dragging = true;
	}

	private void Clickable_MouseUp(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		this.dragging = false;
		if (Vector3.Distance(this.pathStart.position, this.closestPoint) <= ARRIVAL_DISTANCE)
		{
			if (OnArrive != null)
				OnArrive.Invoke(this, 0f);
		}
		else if (Vector3.Distance(this.pathEnd.position, this.closestPoint) <= ARRIVAL_DISTANCE)
		{
			if (OnArrive != null)
				OnArrive.Invoke(this, 1f);
		}
		else
		{
			if (AbortDragging != null)
				AbortDragging.Invoke(this, GetPathPercentage(this.pathStart.position, this.pathEnd.position, this.closestPoint));
		}
	}


	private static float GetPathPercentage(Vector3 start, Vector3 end, Vector3 point)
	{
		return Vector3.Dot(point - start, end - start);
	}

	private static bool IsPointWithinLine(Vector3 start, Vector3 end, Vector3 point)
	{
		Vector3 toPoint = point - start;
		Vector3 toEnd = end - start;

		return
			(toPoint.sqrMagnitude <= toEnd.sqrMagnitude) &&     // Not 'past' endpoint
			(Vector3.Dot(toPoint, toEnd) >= 0);                 // Not 'before' startpoint
	}


	void OnDrawGizmos()
	{
		if (Application.isPlaying && this.dragging)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(this.pathStart.position, this.pathEnd.position);
			Gizmos.color = IsPointWithinLine(this.pathStart.position, this.pathEnd.position, this.closestPoint) ? Color.blue : Color.red;
			Gizmos.DrawWireSphere(this.closestPoint, 0.25f);
		}
	}
}
