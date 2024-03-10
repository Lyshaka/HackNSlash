using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
	public Transform pivot;
	public float time = 0.5f;
	public float angle = 120f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	public void TriggerChest()
	{
		StartCoroutine(ActivateChest(time, angle));
	}

	IEnumerator ActivateChest(float time, float angle)
	{
		float elapsedTime = 0f;

		while (elapsedTime <= time)
		{
			pivot.localRotation = Quaternion.Euler(angle * (elapsedTime / time), 0f, 0f);
			//Vector3.Lerp(Vector3.zero, new Vector3(angle, 0f, 0f), elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
