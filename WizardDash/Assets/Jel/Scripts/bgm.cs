using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
	public AudioSource myAudio;
	public PlayerController pc;
	void Start()
    {
		pc = FindObjectOfType<PlayerController>();
		myAudio = GetComponent<AudioSource>();
	}
	void Update()
	{
		if (pc != null && pc.die)
		{
			myAudio.Stop();
		}
	}
}
