using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Range(1, 10)]
	public int lockpickSkillLevel = 1;

	[HideInInspector]
	public Crate target = null;
	private bool opening = false;

	public void LockpickDone()
	{
		opening = false;
		target.done = true;
		HUD.instance.HideKey();
	}

	private void Update()
	{
		if (opening)
			return;

		if (target != null)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				if (target.done)
					return;
				target.OpenLockScreen(this);
				opening = true;
			}
		}

		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * Time.deltaTime * 5);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.back * Time.deltaTime * 5);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.up * Time.deltaTime * -90);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.up * Time.deltaTime * 90);
		}
	}
}
