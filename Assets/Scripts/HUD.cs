using UnityEngine;

public class HUD : MonoBehaviour
{
	public static HUD instance { get; private set; }
	public GameObject indicator;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
		HideKey();
	}

	public void ShowKey()
	{
		indicator.SetActive(true);
	}
	public void HideKey()
	{
		indicator.SetActive(false);
	}
}
