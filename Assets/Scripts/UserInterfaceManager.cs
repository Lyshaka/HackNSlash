using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
	[Header("Experience")]
	[SerializeField] private Slider xpBar;
	[SerializeField] private TextMeshProUGUI xpText;
	[SerializeField] private TextMeshProUGUI xpLvlText;

	[Header("Statistics")]
	[SerializeField] private TextMeshProUGUI dmg;
	[SerializeField] private TextMeshProUGUI str;
	[SerializeField] private TextMeshProUGUI inte;
	[SerializeField] private TextMeshProUGUI dex;

	[Header("Health")]
	[SerializeField] private Slider hBar;

	[Header("Mana")]
	[SerializeField] private Slider mBar;

	[Header("Spells")]
	[SerializeField] private Image spell0Icon;
	[SerializeField] private Image spell1Icon;
	[SerializeField] private Image spell2Icon;
	[SerializeField] private Sprite none;
	[SerializeField] private Image selectedImage;


	public void UpdateExperience(float amount, float max, int level)
	{
		xpBar.value = amount / max;
		xpText.SetText(amount + " / " + max);
		xpLvlText.SetText("" + level);
	}

	public void UpdateStatistics(Player.Stats stats, int d)
	{
		dmg.SetText("DMG : " + d);
		str.SetText("STR : " + stats.GetStrength());
		inte.SetText("INT : " + stats.GetIntelligence());
		dex.SetText("DEX : " + stats.GetDexterity());
	}

	public void UpdateSpell(List<Spell> spells)
	{
		if (spells.Count >= 3)
		{
			spell0Icon.sprite = (spells[0] != null) ? spells[0].GetImage() : none;
			spell1Icon.sprite = (spells[1] != null) ? spells[1].GetImage() : none;
			spell2Icon.sprite = (spells[2] != null) ? spells[2].GetImage() : none;
		}

	}

	public void UpdateSelectedSpell(int index)
	{
		selectedImage.transform.localPosition = new Vector3(-32.5f + index * 110, 0, 0);
	}

	// // Start is called before the first frame update
	// void Start()
	// {

	// }

	// // Update is called once per frame
	// void Update()
	// {
		
	// }
}
