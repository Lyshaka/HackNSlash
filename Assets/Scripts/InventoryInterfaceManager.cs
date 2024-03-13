using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInterfaceManager : MonoBehaviour
{
	[SerializeField] private GameObject inventoryCanvas;

	[SerializeField] private GameObject panel;
	[SerializeField] private GameObject itemInterfaceObject;

	private bool inventoryState = false;

	public void AddItem(Item item)
	{
		GameObject obj = Instantiate(itemInterfaceObject, panel.transform);
		obj.GetComponent<ItemInterface>().SetItem(item);
	}

	public void ToggleInventory()
	{
		inventoryState = !inventoryState;
		inventoryCanvas.SetActive(inventoryState);
		Time.timeScale = inventoryState ? 0f : 1f;
	}

    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas.SetActive(inventoryState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
		{
			Debug.Log("Test");
			

		}
    }
}
