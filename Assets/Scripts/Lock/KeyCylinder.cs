using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCylinder : MonoBehaviour
{
	public Transform top;
	public Transform bottom;
	public AudioClip shake;
	public AudioClip open;
	public float length { get; private set; }
	private Animator anim;
	private AudioSource aud;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		aud = gameObject.GetComponent<AudioSource>();
	}

	public void CalculateLength()
	{
		length = Mathf.Abs(top.position.y - bottom.position.y);
	}
	public void Shake()
	{
		aud.clip = shake;
		aud.Play();
		anim.Play("Shake", 0, 0);
	}
	public void Open()
	{
		aud.clip = open;
		aud.Play();
		anim.Play("Open");
	}
}
