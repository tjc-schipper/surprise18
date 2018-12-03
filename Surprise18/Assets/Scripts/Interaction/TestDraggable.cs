using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class TestDraggable : MonoBehaviour {


	void Start()
	{
		Draggable d = GetComponent<Draggable>();
		d.OnSnap += D_OnSnap;
	}

	private void D_OnSnap(Draggable sender, Transform snapLocation)
	{
		Debug.Log("Snapped '" + sender.gameObject.name + "' to '" + snapLocation.gameObject.name + "'");
	}
}
