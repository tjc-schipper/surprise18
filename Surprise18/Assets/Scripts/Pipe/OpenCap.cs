using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OpenCap : MonoBehaviour
{

	[SerializeField]
	ParticleSystem bigParticles;

	[SerializeField]
	ParticleSystem smallParticles;

	[SerializeField]
	GameObject hiddenPresent;


	private Animator animator;


	// Use this for initialization
	void Start()
	{
		this.animator = GetComponent<Animator>();
		this.hiddenPresent.SetActive(false);
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			DoOpen();
		}
	}

	public void DoOpen()
	{
		this.animator.SetBool("b_Open", true);
		this.smallParticles.Play();
		DG.Tweening.DOVirtual.DelayedCall(2.5f, () =>
		{
			this.bigParticles.Play();
			this.hiddenPresent.SetActive(true);
		});
	}
}
