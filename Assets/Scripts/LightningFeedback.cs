using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFeedback : MonoBehaviour
{
    private LineRenderer lr;
	[SerializeField] private int nb = 8;
	[SerializeField] private float delta = 0.5f;

	// Start is called before the first frame update
	void Start()
    {
		lr = gameObject.GetComponent<LineRenderer>();
		lr.positionCount = nb;
		for (int i = 0; i < nb; i++)
		{
			lr.SetPosition(i, new Vector3(transform.position.x, transform.position.y + nb, transform.position.z));
		}
		StartCoroutine(SetStrikePosition(lr.positionCount - 1));
	}


    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator SetStrikePosition(int index)
	{
		lr.SetPosition(index, new Vector3(transform.position.x + Random.Range(-delta, delta), index, transform.position.z + Random.Range(-delta, delta)));
		yield return new WaitForSeconds(0.01f);
		if (index > 0)
		{
			StartCoroutine(SetStrikePosition(index - 1));
		}
		else
		{
			StartCoroutine(RemoveStrike());
		}
	}

	IEnumerator RemoveStrike()
	{
		lr.positionCount--;
		yield return new WaitForSeconds(0.01f);
		if (lr.positionCount > 0)
		{
			StartCoroutine(RemoveStrike());
		}
	}
}
