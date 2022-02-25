using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
	private LockBody owner;

	public float speed = 1;
	public Transform top;
	public Transform bottom;
	public AudioClip click;
	public AudioClip tick;

	private AudioSource aud;

	private Vector3 initialPosition;//position where it started
	private Vector3 snapPosition;//goal position. will snap to this position when stuck
	private float positionLimit;//distance it can go(upward only)
	private float snapRange;//offset from goal position that allows pin to snap(up and down) depend on difficulty and player skill

	private Color initialColor;//color it started with

	public float length { get; private set; }

	public bool stuck { get; private set; }

	private void Awake()
	{
		aud = gameObject.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (stuck || owner == null)
			return;

		if(transform.position.y > initialPosition.y)
		{
			transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
			if (transform.position.y <= initialPosition.y)
				Release(false);
		}
		float distance = Mathf.Abs(transform.position.y - snapPosition.y);
		ChangeColor(distance <= snapRange);
	}

	private void CalculateLength()
	{
		length = Mathf.Abs(top.position.y - bottom.position.y);
	}
	private void RandomScale()
	{
		float scale_max;
		switch (owner.difficulty)
		{
			case LockBody.Difficulty.Hard:
				scale_max = (owner.length / length) * 0.9f;
				break;
			case LockBody.Difficulty.Hell:
				scale_max = (owner.length / length) * 0.7f;
				break;
			default:
				scale_max = (owner.length / length);
				break;
		}
		 
		float scale_min = owner.keyCylinder.length / length;
		float scale = Random.Range(scale_min, scale_max);
		transform.localScale = new Vector3(1, scale, 1);
		length *= scale;
	}
	private void RandomPosition()
	{
		float y_max = (owner.bottom.position.y + length)*0.9f;
		float y_min = owner.bottom.position.y;
		float y = Random.Range(y_min, y_max);
		transform.position = new Vector3(transform.position.x, y , transform.position.z);
	}
	private void ChangeColor(bool available)
	{
		if (available)
		{
			transform.Find("Pin").GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
		}
		else
		{
			transform.Find("Pin").GetComponent<Renderer>().material.color = initialColor;
		}
	}
	public void Push()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
		if (transform.position.y > positionLimit)
		{
			transform.position = new Vector3(transform.position.x, positionLimit, transform.position.z);
		}
	}
	public void Release(bool playSound)
	{
		stuck = false;
		transform.position = initialPosition;
		if (playSound)
		{
			aud.clip = tick;
			aud.Play();
		}
	}
	public bool Stuck()
	{
		float distance = Mathf.Abs(transform.position.y - snapPosition.y);
		if (distance <= snapRange)
		{
			stuck = true;
			aud.clip = click;
			aud.Play();
			transform.position = snapPosition;
		}
		else
		{
			if (owner.difficulty == LockBody.Difficulty.Hell)
			{
				owner.ReleaseAll();
			}
			Release(true);
		}
		return stuck;
	}
	public void SetOwner(LockBody _owner)
	{
		owner = _owner;
	}
	public void Setup()
	{
		CalculateLength();
		RandomScale();
		RandomPosition();

		initialPosition = transform.position;
		positionLimit = owner.top.position.y;
		snapPosition = new Vector3(transform.position.x, owner.bottom.position.y + length, transform.position.z);

		switch (owner.difficulty)
		{
			case LockBody.Difficulty.Hard:
				snapRange = (transform.localScale.y * length) * 0.3f;//30% of length 
				break;
			case LockBody.Difficulty.Hell:
				snapRange = (transform.localScale.y * length) * 0.15f;//30% of length 
				break;
			default:
				snapRange = (transform.localScale.y * length) * 0.5f;//30% of length 
				break;
		}

		initialColor = transform.Find("Pin").GetComponent<Renderer>().material.color;
	}
}
