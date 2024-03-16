using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	private Transform pivot;
    // Start is called before the first frame update
    void Start()
    {
        pivot = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pivot.LookAt(Camera.main.transform);
    }
}
