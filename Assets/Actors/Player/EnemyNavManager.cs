using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavManager : MonoBehaviour
{
	private List<GameObject> enemies = new List<GameObject>(); //SerializeField causes some errors :s


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			if (!enemies.Contains(other.gameObject.transform.parent.gameObject))
			{
				enemies.Add(other.gameObject.transform.parent.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			GameObject obj = other.gameObject.transform.parent.gameObject;
			if (enemies.Contains(obj) && obj != null)
			{
				DeleteFromList(obj);
			}
		}
	}

	public void DeleteFromList(GameObject obj)
	{
		if (obj != null && enemies.Contains(obj))
		{
			enemies.Remove(obj);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		foreach (GameObject enemy in enemies)
		{
			enemy.GetComponent<NavMeshAgent>().SetDestination(transform.position);
		}
	}
}
