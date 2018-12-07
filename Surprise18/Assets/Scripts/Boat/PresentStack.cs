using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PresentStack : MonoBehaviour
{

	public delegate void VoidEvent();
	public event VoidEvent OnExploded;

	[SerializeField]
	AudioClip clip_Explode;

	[SerializeField]
	private Transform explosionPoint;

	[SerializeField]
	private float explosionForce = 10f;

	[SerializeField]
	private float explosionRadius = 2f;


	public void Explode()
	{
		AudioSource.PlayClipAtPoint(this.clip_Explode, Camera.main.transform.position, 1f);

		Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody r in rbs)
		{
			r.isKinematic = false;
			r.AddExplosionForce(this.explosionForce, this.explosionPoint.position, this.explosionRadius);
			r.AddForce(Vector3.up * 1f, ForceMode.Impulse);
		}

		DOVirtual.DelayedCall(4f, () =>
		{
			for (int i = 0; i < rbs.Length; i++)
			{
				Destroy(rbs[i].gameObject);
			}
		});

		if (this.OnExploded != null)
			this.OnExploded.Invoke();
	}
}
