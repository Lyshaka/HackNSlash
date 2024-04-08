using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTracker : MonoBehaviour
{
	[Header("Properties")]
	[SerializeField] private GameObject objectToTrack;
	[SerializeField] private string fileName = "PathTracker";
	[SerializeField][Range(0, 10)] private float timeBetweenTrack = 0.5f;

	[SerializeField]
	private List<Vector3> positions = new List<Vector3>();

	void OnApplicationQuit()
	{
		PathTrackerData.SaveData(positions, fileName);
	}

	IEnumerator Track()
	{
		positions.Add(objectToTrack.transform.position);
		yield return new WaitForSeconds(timeBetweenTrack);
		StartCoroutine(Track());
	}

	void Start()
	{
		StartCoroutine(Track());
	}
}
