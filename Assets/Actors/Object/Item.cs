using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Item
{
	[SerializeField] protected int id;
	[SerializeField] protected string name;
	[SerializeField] protected int strength;
	[SerializeField] protected int intelligence;
	[SerializeField] protected int dexterity;


	public Item(string[] data)
	{
		id = int.Parse(data[0]);
		name = data[1];
		strength = int.Parse(data[2]);
		intelligence = int.Parse(data[3]);
		dexterity = int.Parse(data[4]);
	}

	public int GetID()
	{
		return (id);
	}

	public string GetName()
	{
		return (name);
	}

	public int GetStrength()
	{
		return (strength);
	}

	public int GetIntelligence()
	{
		return (intelligence);
	}

	public int GetDexterity()
	{
		return (dexterity);
	}
}
