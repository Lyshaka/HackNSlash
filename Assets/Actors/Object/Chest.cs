using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

[Serializable]
public class Chest : InteractiveObject
{
	private Item item;
	public int ID;
	private Manager manager;
	private InventoryInterfaceManager playerInventory;

	public Item ChestUseObject()
	{
		GetComponent<OpenChest>().TriggerChest();
		if (UseObject() && ID != 0)
		{
			string data = manager.GetItem(ID);
			item = LoadData.CreateItemData(data);
			Debug.Log("You received : " + item.GetName());
			GetComponent<ParticleSystem>().Play();
			return (item);
		}
		return (null);
	}

	// Start is called before the first frame update
	void Start()
	{
		activable = true;
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		playerInventory = GameObject.Find("PlayerCharacter").GetComponent<Player>().GetInventory();
	}
}
