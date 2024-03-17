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
	[SerializeField] private TextMeshProUGUI hText;

	[Header("Mana")]
	[SerializeField] private float oomTime = 0.5f;
	[SerializeField] private Slider mBar;
	[SerializeField] private TextMeshProUGUI mText;
	[SerializeField] private Image oom;
	private Coroutine OOMCoroutine;

	[Header("Spells")]
	[SerializeField] private Image spell0Icon;
	[SerializeField] private TextMeshProUGUI spell0Cost;
	[SerializeField] private Image spell1Icon;
	[SerializeField] private TextMeshProUGUI spell1Cost;
	[SerializeField] private Image spell2Icon;
	[SerializeField] private TextMeshProUGUI spell2Cost;
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
			spell0Cost.SetText("" + spells[0].GetCost());
			spell1Icon.sprite = (spells[1] != null) ? spells[1].GetImage() : none;
			spell1Cost.SetText("" + spells[1].GetCost());
			spell2Icon.sprite = (spells[2] != null) ? spells[2].GetImage() : none;
			spell2Cost.SetText("" + spells[2].GetCost());
		}

	}

	public void UpdateHealth(float h, float hMax)
	{
		hBar.value = h / hMax;
		hText.SetText((int)h + "/" + hMax);
	}

	public void UpdateMana(float m, float mMax)
	{
		mBar.value = m / mMax;
		mText.SetText((int)m + "/" + mMax);
	}

	public void UpdateSelectedSpell(int index)
	{
		selectedImage.transform.localPosition = new Vector3(-32.5f + index * 110, 0, 0);
	}

	public void IsOOM()
	{
		if (OOMCoroutine != null)
			StopCoroutine(OOMCoroutine);
		OOMCoroutine = StartCoroutine(PlayOOM());
	}

	IEnumerator PlayOOM()
	{
		float elapsedTime = 0;

		while (elapsedTime < oomTime)
		{
			oom.color = new Color(oom.color.r, oom.color.g, oom.color.b, 1 - (elapsedTime / oomTime));
			//Debug.Log("Color : " + oom.color.a);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
