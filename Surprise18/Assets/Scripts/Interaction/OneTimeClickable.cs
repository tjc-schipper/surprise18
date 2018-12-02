using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable))]
public class OneTimeClickable : MonoBehaviour
{

	[SerializeField]
	private UnityEngine.Events.UnityEvent OnTrigger;

	private Clickable clickable;
	private bool triggered = false;


	void Start()
	{
		this.clickable = GetComponent<Clickable>();
		this.clickable.Click += Clickable_Click;
	}

	private void Clickable_Click(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		if (!this.triggered)
		{
			this.triggered = true;
			if (OnTrigger != null)
				OnTrigger.Invoke();
		}
	}
}
