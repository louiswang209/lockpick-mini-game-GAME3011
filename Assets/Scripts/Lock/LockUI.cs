using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockUI : MonoBehaviour
{
	public static LockUI instance { get; private set; }

	public RectTransform timer;
	public TextMeshProUGUI progress;
	public TextMeshProUGUI success;

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
	}

	public void TimerScale(float percentage)
	{
		timer.localScale = new Vector3(percentage, 1, 1);
	}
	public void ProgressText(float prog)
	{
		progress.text = (prog * 100).ToString() + "%";
	}
	public void SuccessText(float succ)
	{
		success.text = (succ * 100).ToString() + "%";
	}
}