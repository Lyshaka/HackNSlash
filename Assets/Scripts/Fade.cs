using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fade : MonoBehaviour
{
	[SerializeField] private float speed = 2f;
	[SerializeField] private float time = 0.8f;

	[SerializeField] private TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeText());
    }

	public void SetText(string text)
	{
		tmp.text = text;
	}

	IEnumerator FadeText()
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
