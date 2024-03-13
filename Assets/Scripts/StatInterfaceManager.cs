using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatInterfaceManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI statsText;

	[SerializeField] private string damageText = "Damage";
	[SerializeField] private string hpRegenText = "HP Regen";
	[SerializeField] private string manaRegenText = "Mana Regen";
	[SerializeField] private string strText = "Strength";
	[SerializeField] private string inteText = "Intelligence";
	[SerializeField] private string dexText = "Dexterity";
	[SerializeField] private string nbItemText = "Item Numbers";

	public void UpdateText(Player player)
	{
		string text = "";
		text += damageText + " : " + player.GetDamage() + "\n";
		text += hpRegenText + " : " + player.GetHpRegen() + "/s\n";
		text += manaRegenText + " : " + player.GetManaRegen() + "/s\n";
		text += strText + " : " + player.GetStats().GetStrength() + "\n";
		text += inteText + " : " + player.GetStats().GetIntelligence() + "\n";
		text += dexText + " : " + player.GetStats().GetDexterity() + "\n";
		text += nbItemText + " : " + player.GetNbItem() + "\n";
		statsText.SetText(text);
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
