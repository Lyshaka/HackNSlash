using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
	[Range(0f, 1f)]
	[SerializeField] private float value;
	[SerializeField] private float renderTime = 1f;

	[Header("Camera follow lock on axis")]
	[SerializeField] private bool x;
	[SerializeField] private bool y;
	[SerializeField] private bool z;

	private float axisX;
	private float axisY;
	private float axisZ;

	private Slider slider;
	private Canvas canvas;
	private Coroutine coroutine;

	// Start is called before the first frame update
	void Start()
	{
		slider = GetComponentInChildren<Slider>();
		canvas = GetComponentInChildren<Canvas>();
		canvas.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		RotateTowardsCamera();
	}

	public void SetPercent(float percentage)
	{
		slider.value = Mathf.Clamp(percentage, 0f, 1f);
	}

	public void UpdateHealthBar(float value, float max)
	{
		this.value = Mathf.Clamp(value / max, 0f, 1f);
		slider.value = this.value;
		if (coroutine != null)
		{
			StopCoroutine(coroutine);
		}
		coroutine = StartCoroutine(RenderBar(renderTime));
	}

	void RotateTowardsCamera()
	{
		axisX = x ? transform.position.x : Camera.main.transform.position.x;
		axisY = y ? transform.position.y : Camera.main.transform.position.y;
		axisZ = z ? transform.position.z : Camera.main.transform.position.z;
		transform.LookAt(new Vector3(axisX, axisY, axisZ), Vector3.down);
	}

	IEnumerator RenderBar(float time)
	{
		canvas.enabled = true;
		yield return new WaitForSeconds(time);
		canvas.enabled = false;
	}
}
