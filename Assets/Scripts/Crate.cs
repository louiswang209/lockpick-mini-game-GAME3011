using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
	public LockBody.Difficulty difficulty = LockBody.Difficulty.Hard;
	public GameObject lockObject;

	[HideInInspector]
	public bool done = false;

	public void OpenLockScreen(Player player)
	{
		if (done)
			return;
		GameObject temp = Instantiate(lockObject);
		temp.transform.Find("Body").GetComponent<LockBody>().difficulty = difficulty;
		temp.transform.Find("Body").GetComponent<LockBody>().Init(player);
	}

	private void OnTriggerEnter(Collider col)
	{
		if (done)
			return;
		Player p = col.GetComponent<Player>();
		if (p != null)
		{
			HUD.instance.ShowKey();
			p.target = this;
		}
	}

	private void OnTriggerExit(Collider col)
	{
		Player p = col.GetComponent<Player>();
		if (p != null)
		{
			HUD.instance.HideKey();
			p.target = null;
		}
	}
}
