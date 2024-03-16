using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

	[SerializeField] private Enemy enemy;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 3)
		{
			enemy.SetPlayer(other.gameObject.GetComponent<Player>());
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == 3)
		{
			enemy.SetPlayer(null);
		}
	}
}
