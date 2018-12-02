using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToFitCamera : MonoBehaviour
{

	[SerializeField]
	private float distanceFromCamera = 50f;

	private Camera cam;
	private const float SAFETY_BUFFER = 1.5f;

	void Start()
	{
		this.cam = this.transform.parent.GetComponent<Camera>();
		this.transform.localPosition = new Vector3(0f, 0f, this.distanceFromCamera);
		float size = this.distanceFromCamera * Mathf.Tan(Mathf.Deg2Rad * this.cam.fieldOfView) * SAFETY_BUFFER;
		this.transform.localScale = new Vector3(size, size, 1f);
	}
}
