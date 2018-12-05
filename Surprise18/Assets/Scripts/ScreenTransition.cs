using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenTransition : MonoBehaviour
{

	[SerializeField]
	private Shader brightnessShader;


	private Material brightnessMaterial;
	private Tween currentTransition;

	void Start()
	{
		this.brightnessMaterial = new Material(this.brightnessShader);
		this.brightnessMaterial.SetFloat("_Brightness", 0.0f);
		DoTransition(1f, 2f);   // Do initial transition to fade in game!
	}


	public void DoTransition(float targetBrightness, float duration, float delay = 0f)
	{
		if (this.currentTransition != null)
		{
			this.currentTransition.Kill(false);
			this.currentTransition = null;
		}

		this.currentTransition = this.brightnessMaterial.DOFloat(targetBrightness, "_Brightness", duration)
			.SetDelay(delay)
			.OnComplete(() =>
			{
				this.currentTransition = null;
			});
	}


	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst, this.brightnessMaterial);
	}
}
