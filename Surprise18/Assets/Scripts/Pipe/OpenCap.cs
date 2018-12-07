using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OpenCap : MonoBehaviour
{

	[SerializeField]
	AudioClip clip_Squeak;


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
			AudioSource.PlayClipAtPoint(this.clip_Squeak, Camera.main.transform.position, 1f);
			this.hiddenPresent.SetActive(true);
		});
	}
}
