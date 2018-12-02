using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class Clickable : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public delegate void ClickEvent(Clickable sender, PointerEventData e);

	public event ClickEvent Click;
	public event ClickEvent MouseEnter;
	public event ClickEvent MouseExit;

	public event ClickEvent MouseDown;
	public event ClickEvent MouseUp;


	private bool IsCoveredByUI()
	{
		// Prevent clicking if covered by UI element
		UnityEngine.EventSystems.EventSystem es = UnityEngine.EventSystems.EventSystem.current;
		return (es != null && es.IsPointerOverGameObject());
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		if (this.Click != null)
			this.Click.Invoke(this, eventData);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (MouseDown != null)
			MouseDown.Invoke(this, eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (MouseUp != null)
			MouseUp.Invoke(this, eventData);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (MouseEnter != null)
			MouseEnter.Invoke(this, eventData);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (MouseExit != null)
			MouseExit.Invoke(this, eventData);
	}


}
