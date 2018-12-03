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

    [SerializeField]
    private float cameraDistance = 10f;

    [SerializeField]
    private Vector2 xBounds = new Vector2(-35f, 35f);


    private float angleX;
    private float angleY;
    private bool dragging = false;
    private Vector2 lastMousePos;


    void OnEnable()
    {
        if (this.clickable != null)
        {
            this.clickable.MouseDown += Clickable_MouseDown;
            this.clickable.MouseUp += Clickable_MouseUp;
        }

        Vector3 pivotToCam = this.transform.position - this.pivotPoint.position;

        this.angleX = Mathf.Asin(pivotToCam.y) * Mathf.Rad2Deg;
        this.angleY = Mathf.Asin(pivotToCam.x) * Mathf.Rad2Deg;
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

            this.angleX = Mathf.Clamp(this.angleX - deltaPos.y * this.sensitivity.y, this.xBounds.x, this.xBounds.y);
            this.angleY = this.angleY + deltaPos.x * this.sensitivity.x;

            this.transform.position = this.pivotPoint.position + new Vector3(
                 Mathf.Sin(this.angleY * Mathf.Deg2Rad),
                 Mathf.Sin(this.angleX * Mathf.Deg2Rad),
                 Mathf.Cos(this.angleY * Mathf.Deg2Rad)) * this.cameraDistance;
            this.transform.LookAt(this.pivotPoint.position);

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
