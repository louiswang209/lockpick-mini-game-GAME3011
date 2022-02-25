using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBody : MonoBehaviour
{
	public enum Difficulty
	{
		Hard,
		Hell
	}
	public Transform top;
	public Transform bottom;
	public float length { get; private set; }

	public Difficulty difficulty;
	[Range(10.0f, 60.0f)]
	public float timeLimit;
	private float timer;

	[HideInInspector]
	public Player opener;

	private bool open = false;

	public Pin[] pins;
	public KeyCylinder keyCylinder;
	public Lockpick lockpick;

	public int pinIndex { get; private set; }

	public void Init(Player player)
	{
		timer = timeLimit;
		opener = player;
		lockpick.SetLockBody(this);
		CalculateLength();
		keyCylinder.CalculateLength();
		foreach (Pin p in pins)
		{
			p.SetOwner(this);
			p.Setup();
		}
	}

	private void Update()
	{
		if (open)
			return;
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			Done();
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (pins[pinIndex].Stuck())
			{
				pinIndex++;
				if (pinIndex == pins.Length)
				{
					LockUI.instance.ProgressText(1.0f);
					pinIndex = 0;
					keyCylinder.Open();
					open = true;
					Invoke("Done", 3);
					return;
				}
			}
			else
			{
				keyCylinder.Shake();
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			pins[pinIndex].Push();
		}

		LockUI.instance.TimerScale(timer / timeLimit);
		LockUI.instance.ProgressText(((float)(pinIndex))/((float)pins.Length));
		LockUI.instance.SuccessText(0.5f);
	}

	private void CalculateLength()
	{
		length = Mathf.Abs(top.position.y - bottom.position.y);
	}
	private void Done()
	{
		opener.LockpickDone();
		Destroy(transform.parent.gameObject);
	}

	public void ReleaseAll()
	{
		foreach (Pin p in pins)
		{
			p.Release(true);
		}
		pinIndex = 0;
	}
}
