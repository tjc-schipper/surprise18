using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable))]
public class Draggable : MonoBehaviour
{

    [SerializeField] private Transform originalLocation;

    [SerializeField] private float zDistance = 2f;

    [SerializeField] private float lerpFactor = 1f;


    private Clickable clickable;
    private bool dragging = false;

    private Transform snapLocation;
    private Collider[] activeConstraints;
    private Rigidbody cursorAnchor;
    private Vector3 dragPos;

    private const int IDX_CURSOR = 0;
    private const int IDX_SNAP = 1;

    private Ray currentRay;


    private void OnEnable()
    {
        this.clickable = GetComponent<Clickable>();
        this.clickable.MouseDown += Clickable_MouseDown;
        this.activeConstraints = new Collider[2];

        this.snapLocation = this.originalLocation;
    }

    private void OnDisable()
    {
        if (this.clickable != null)
        {
            this.clickable.MouseDown -= Clickable_MouseDown;
            this.clickable.MouseUp -= Clickable_MouseUp;
        }

        this.activeConstraints = null;
    }


    private void LateUpdate()
    {
        Vector3 targetPos = this.snapLocation.position;
        Quaternion targetRot = this.snapLocation.rotation;

        if (this.dragging)
        {
            // Cancel drag if mouse button not down!
            if (Input.GetMouseButton(0) == false)
            {
                Clickable_MouseUp(null, null);
                return;
            }


            // Update anchor position to match mouse pos
            Vector3 wsCursorPos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                this.zDistance
                ));
            this.dragPos = wsCursorPos;
            this.cursorAnchor.MovePosition(this.dragPos);


            // Do a raycast for any snap targets
            RaycastHit hit;
            Vector3 wsMousePos = Input.mousePosition;
            wsMousePos.z = this.zDistance;
            this.currentRay = new Ray(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(wsMousePos) - Camera.main.transform.position);

            if (Physics.Raycast(
                ray: this.currentRay,
                hitInfo: out hit,
                maxDistance: 500f,
                layerMask: LayerMask.GetMask("SnapCollider")
                ))
            {
                Debug.Log("Hitting collider: " + hit.collider.gameObject.name);
                if (hit.collider != this.activeConstraints[IDX_SNAP])
                {
                    RemoveConstraint(IDX_SNAP);
                    SetConstraint(IDX_SNAP, hit.collider);
                }
            }
            else
            {
                RemoveConstraint(IDX_SNAP);
            }


            Vector3 blendPos = this.activeConstraints[0].transform.position;
            Quaternion blendRot = this.activeConstraints[0].transform.rotation;

            // Get inbetween orientation to indicate that there are now two constraints acting upon the draggable
            if (this.activeConstraints[1] != null)
            {
                blendPos = Vector3.Lerp(blendPos, this.activeConstraints[1].transform.position, 0.85f);
                blendRot = Quaternion.Slerp(blendRot, this.activeConstraints[1].transform.rotation, 0.85f);
            }

            targetPos = blendPos;
            targetRot = blendRot;
        }

        // Lerp the Draggable towards the targetPos/Rot
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, this.lerpFactor * Time.deltaTime);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, this.lerpFactor * Time.deltaTime);
    }


    private void Clickable_MouseDown(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
    {
        if (!this.dragging)
        {
            this.dragging = true;

            GameObject go = new GameObject("_CursorAnchor");
            this.cursorAnchor = go.AddComponent<Rigidbody>();
            this.cursorAnchor.isKinematic = true;
            this.cursorAnchor.useGravity = false;
            Collider c = go.AddComponent<BoxCollider>();

            SetConstraint(IDX_CURSOR, c);
        }
    }

    private void Clickable_MouseUp(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
    {
        if (this.dragging)
        {
            this.dragging = false;
            Destroy(this.cursorAnchor.gameObject);

            RemoveConstraint(IDX_CURSOR);

            // Are we currently hovering over a snapCollider? If so, set that as our target position. If not, return to originalLocation
            if (this.activeConstraints != null && this.activeConstraints[1] != null)
                this.snapLocation = this.activeConstraints[1].transform;
            else
                this.snapLocation = this.originalLocation;
        }
    }


    private void SetConstraint(int constraint, Collider other)
    {
        if (this.activeConstraints != null)
        {
            this.activeConstraints[constraint] = other;
        }
    }

    private void RemoveConstraint(int constraint)
    {
        if (this.activeConstraints != null)
        {
            this.activeConstraints[constraint] = null;
        }
    }


    private void OnGUI()
    {
        string s = "Constraints: ";
        if (this.activeConstraints != null)
        {
            if (this.activeConstraints[0] != null)
                s += "\n[0] " + this.activeConstraints[0].gameObject.name;

            if (this.activeConstraints[1] != null)
                s += "\n[1] " + this.activeConstraints[1].gameObject.name;
        }
        GUI.Label(new Rect(8f, 8f, 128f, 64f), s);
    }

    private void OnDrawGizmos()
    {
        if (this.activeConstraints != null)
        {
            if (this.activeConstraints[0] != null)
                Gizmos.DrawLine(this.transform.position, this.activeConstraints[0].transform.position);

            if (this.activeConstraints[1] != null)
                Gizmos.DrawLine(this.transform.position, this.activeConstraints[1].transform.position);
        }
    }

}
