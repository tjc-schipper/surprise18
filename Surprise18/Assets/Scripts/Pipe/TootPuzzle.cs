using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class TootPuzzle : MonoBehaviour
{

	[SerializeField]
	TootButton[] buttons;

	[SerializeField]
	ParticleSystem particles;

	[SerializeField]
	AudioClip[] audioClips;


	[SerializeField]
	OpenCap openCap;


	private AudioSource source;
	private int waitingForIdx = 0;


	void OnEnable()
	{
		foreach (TootButton button in this.buttons)
		{
			button.enabled = true;
			button.OnToot += Button_OnToot;
		}
	}

	void OnDisable()
	{
		foreach (TootButton button in this.buttons)
		{
			if (button != null)
			{
				button.enabled = false;
				button.OnToot -= Button_OnToot;
			}
		}
	}

	void Start()
	{
		this.source = GetComponent<AudioSource>();
		this.source.playOnAwake = false;
	}


	private void Button_OnToot(TootButton sender, int idx)
	{
		AudioClip clip = this.audioClips[Mathf.Clamp(idx, 0, 3)];
		this.source.clip = clip;
		this.source.Play();
		this.particles.Play();

		if (idx == this.waitingForIdx)
		{
			if (idx == 3)
			{
				CompletePuzzle();
			}
			else
			{
				this.waitingForIdx++;
			}
		}
		else
		{
			this.waitingForIdx = 0;
		}
	}


	private void CompletePuzzle()
	{
		this.openCap.DoOpen();
		this.enabled = false;
		DOVirtual.DelayedCall(2.5f, () =>
		{
			foreach (AudioClip clip in this.audioClips)
			{
				AudioSource.PlayClipAtPoint(clip, this.transform.position, 1.0f);
			}
		});
	}
}
