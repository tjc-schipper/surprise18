using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poems : MonoBehaviour
{

	[SerializeField]
	PoemScreen[] poemScreens;


	public static void ShowPoem(int idx)
	{
		GameObject.FindObjectOfType<Poems>().DoShow(idx);
	}

	public void DoShow(int idx)
	{
		this.poemScreens[idx].enabled = true;
	}
}
