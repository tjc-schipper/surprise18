using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathDraggable))]
public class TestPathDraggable : MonoBehaviour
{

	void Start()
	{
		PathDraggable p = GetComponent<PathDraggable>();
		p.OnArrive += P_OnArrive;
	}

	private void P_OnArrive(PathDraggable sender, float percentage)
	{
		string location = (percentage.Equals(1f)) ? "END" : "START";
		Debug.Log("ARRIVED: " + location + " (" + percentage.ToString() + ")");
	}
}
