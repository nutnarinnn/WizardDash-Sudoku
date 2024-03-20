using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Animator anim;

	void Start()
    {
		anim = GetComponent<Animator>();
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			anim.SetBool("isWin", true);
		}
	}
}
