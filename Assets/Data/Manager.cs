using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	[SerializeField] private string[] items;
	[SerializeField] private string[] spells;

	// Start is called before the first frame update
	void Awake()
	{
		items = LoadData.ReadString("Items.tsv");
		spells = LoadData.ReadString("Skills.tsv");
		//foreach (string spell in spells)
		//{
		//	Debug.Log("Spell : " + spell);
		//}
		
	}

	public string GetItem(int id)
	{
		return (items[id]);
	}

	public string GetSpell(int id)
	{
		return (spells[id + 1]);
	}
}
