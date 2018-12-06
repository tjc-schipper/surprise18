using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAnimations : MonoBehaviour
{

    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    private Vector3 basePosition;

    public void SetLoad(float percentage)
    {
        float y = Mathf.Lerp(this.maxHeight, this.minHeight, percentage);
        this.transform.position = new Vector3(
            this.transform.position.x,
            y,
            this.transform.position.z);
    }
}
