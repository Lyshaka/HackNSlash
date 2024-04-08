using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{
	[SerializeField]
	private LineRenderer line;

	[SerializeField]
	private Vector3[] positions;

	public void DrawLine(Vector3[] positions)
	{
		line.positionCount = positions.Length;
		for (int i = 0; i < positions.Length; i++)
		{
			line.SetPosition(i, positions[i]);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
