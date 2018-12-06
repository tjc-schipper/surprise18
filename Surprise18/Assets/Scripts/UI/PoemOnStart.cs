using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemOnStart : MonoBehaviour {

	[SerializeField]
	int poemIdx;
	
	void Start () {
		Poems.ShowPoem(this.poemIdx);	
	}
}
