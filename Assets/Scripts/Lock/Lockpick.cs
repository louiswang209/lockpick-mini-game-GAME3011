using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpick : MonoBehaviour
{
	private LockBody lockBody;

	private void Update()
	{
		if(lockBody != null)
			MoveToPinEnd();
	}

	private void MoveToPinEnd()
	{
		transform.position = lockBody.pins[lockBody.pinIndex].bottom.position;
	}

	public void SetLockBody(LockBody _lockBody)
	{
		lockBody = _lockBody;
	}
}
