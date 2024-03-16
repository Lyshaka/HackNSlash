using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private Transform tr;
	[SerializeField] private float time = 0.5f;
	private float height;

	public void Open()
	{
		StartCoroutine(OpenDoor(time));
	}

    // Start is called before the first frame update
    void Start()
    {
        height = tr.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator OpenDoor(float time)
	{
		float elapsedTime = 0f;

		while (elapsedTime < time)
		{
			tr.localPosition = new Vector3(0, height - (elapsedTime / time) * 2, 0);
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
