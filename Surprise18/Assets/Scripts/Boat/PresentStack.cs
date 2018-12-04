using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PresentStack : MonoBehaviour
{

	public delegate void VoidEvent();
	public event VoidEvent OnExploded;

	[SerializeField]
	private Transform explosionPoint;

	[SerializeField]
	private float explosionForce = 10f;

	[SerializeField]
	private float explosionRadius = 2f;


	public void Explode()
	{
		Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody r in rbs)
		{
			r.AddExplosionForce(this.explosionForce, this.explosionPoint.position, this.explosionRadius);
		}

		DOVirtual.DelayedCall(4f, () =>
		{
			for (int i = 0; i < rbs.Length; i++)
			{
				Destroy(rbs[i].gameObject);
			}
		});

		DOVirtual.DelayedCall(1.5f, () =>
		{
			if (this.OnExploded != null)
				this.OnExploded.Invoke();
		});
	}
}
