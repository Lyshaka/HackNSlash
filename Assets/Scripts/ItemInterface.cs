using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInterface : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI itemID;
	[SerializeField] private TextMeshProUGUI itemName;
	[SerializeField] private TextMeshProUGUI strText;
	[SerializeField] private TextMeshProUGUI intText;
	[SerializeField] private TextMeshProUGUI dexText;

	public void SetItem(Item item)
	{
		itemID.SetText("[ID : " + item.GetID() + "]");
		itemName.SetText(item.GetName());
		strText.SetText("[STR : " + item.GetStrength() + "]");
		intText.SetText("[INT : " + item.GetIntelligence() + "]");
		dexText.SetText("[DEX : " + item.GetDexterity() + "]");
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
