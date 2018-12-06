using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TootButton : MonoBehaviour
{

	public delegate void TootEvent(TootButton sender, int idx);
	public event TootEvent OnToot;

	[SerializeField]
	Transform container;

	[SerializeField]
	Clickable clickable;

	[SerializeField]
	int buttonIndex;


	private Sequence seq;


	void OnEnable()
	{
		this.clickable.Click += Clickable_Click;
		EnableButton();
	}

	void OnDisable()
	{
		this.clickable.Click -= Clickable_Click;
		// Delay so animations don't start overlapping on the last button in the sequence!
		DOVirtual.DelayedCall(1f, () =>
		{
			DisableButton();
		});
	}


	private void Clickable_Click(Clickable sender, UnityEngine.EventSystems.PointerEventData e)
	{
		if (this.seq == null)
		{
			PressButton();
		}
	}


	private void EnableButton()
	{
		this.gameObject.SetActive(true);
		this.container.DOLocalMoveY(0f, 1f)
			.SetEase(Ease.OutBounce)
			.OnComplete(() =>
			{
				this.clickable.enabled = true;
			});
	}

	private void DisableButton()
	{
		this.clickable.enabled = false;
		this.container.DOLocalMoveY(-0.1f, 1f)
			.SetEase(Ease.InBack)
			.OnComplete(() =>
			{
				this.gameObject.SetActive(false);
			});
	}

	private void PressButton()
	{
		this.seq = DOTween.Sequence();
		seq.Append(this.container.DOLocalMoveY(-0.025f, 0.25f)
			.SetEase(Ease.OutQuad)
			.OnComplete(() =>
			{
				if (OnToot != null)
					OnToot.Invoke(this, this.buttonIndex);
			}));
		seq.AppendInterval(1f);
		seq.Append(this.container.DOLocalMoveY(0f, 0.5f)
			.SetEase(Ease.OutBounce));

		seq.OnComplete(() =>
		{
			this.seq = null;
		});

		seq.Play();


	}
}
