using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLever : MonoBehaviour
{
	[SerializeField] private Transform leverPivot;
	[SerializeField] private float time = 0.5f;
	[SerializeField] private float angle = 30f;

    // Start is called before the first frame update
    void Start()
    {
        leverPivot.localEulerAngles = new Vector3(0, 0, -angle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TriggerLever()
	{
		StartCoroutine(Lever(time, angle));
	}

	IEnumerator Lever(float time, float angle)
	{
		float elapsedTime = 0f;

		while (elapsedTime < time)
		{
			leverPivot.localEulerAngles = new Vector3(0f, 0f, angle * 2 * (elapsedTime / time) - angle);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
