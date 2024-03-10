using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	string[] items;
	string[] spells;

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
		return (items[id + 1]);
	}

	public string GetSpell(int id)
	{
		return (spells[id + 1]);
	}
}
