using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCamOrbit : MonoBehaviour {

	[SerializeField]
	Transform pivot;

	[SerializeField]
	float ySpeed = 1f;

	[SerializeField]
	float angleX = 25f;

	[SerializeField]
	float distance = 1f;


	private float angleY = 0f;


	// Update is called once per frame
	void LateUpdate () {
		this.angleY += this.ySpeed * Time.deltaTime;
		Vector3 camPos = this.pivot.position + Quaternion.Euler(this.angleX, this.angleY, 0f) * this.pivot.forward * this.distance;
		this.transform.position = camPos;
		this.transform.LookAt(this.pivot);
	}
}
