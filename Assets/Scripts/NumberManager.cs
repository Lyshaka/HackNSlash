using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class NumberManager : MonoBehaviour
{
	[SerializeField] private float number;
	[SerializeField] private float speed = 2f;

	private float time = 0.8f;

	private TextMeshPro tmp;

	// Start is called before the first frame update
	void Start()
	{
		tmp = GetComponentInChildren<TextMeshPro>();
		tmp.text = number.ToString();
		StartCoroutine(Fade());
	}

	// Update is called once per frame
	void Update()
	{
		transform.rotation = Camera.main.transform.rotation;
	}

	public void SetNumber(float nb)
	{
		number = (float)Math.Round(nb, 1);
	}

	IEnumerator Fade()
	{
		float elapsedTime = 0f;
		while (elapsedTime < time)
		{
			tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f - elapsedTime / time);
			transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * speed), transform.position.z);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
}
