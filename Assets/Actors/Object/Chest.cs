using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Chest : InteractiveObject
{
	private Item item;
	public int ID;
	private Manager manager;

	public Item ChestUseObject()
	{
		if (UseObject())
		{
			GetComponent<OpenChest>().TriggerChest();
			string data = manager.GetItem(ID);
			item = LoadData.CreateItemData(data);
			Debug.Log("You received : " + item.GetName());
			return (item);
		}
		return (null);
	}

	// Start is called before the first frame update
	void Start()
	{
		activable = true;
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}
}
