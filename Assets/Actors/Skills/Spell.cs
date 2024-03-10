using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Spell : Skills
{
	public Spell(string[] datas)		//Constructeur du spell
	{
		string[] str;
		id = int.Parse(datas[0]);								//ID du sort
		name = datas[1];										//Nom du sort
		type = datas[2];										//Type du sort
		subType = datas[3];										//Sous-type du sort
		levelMax = int.Parse(datas[4]);							//Niveau maximum du sort
		damage = float.Parse(datas[5]);							//Dégâts du sort
		str = datas[6].Split('%');
		strength = float.Parse(str[0]) / 100f;					//Scaling sur la force
		str = datas[7].Split('%');
		intelligence = float.Parse(str[0]) / 100f;				//Scaling sur l'intelligence
		str = datas[8].Split('%');
		dexterity = float.Parse(str[0]) / 100f;					//Scaling sur la dextérité
		cost = float.Parse(datas[9]);							//Cout du sort (en mana)
		castTime = float.Parse(datas[10]);						//Temps de cast
		level = 1;												//Niveau de base du sort
		icon = Resources.Load<Sprite>("icon");
		//GetIcon(datas[11]);
		//Debug.Log("Icone : " + icon);
		//Debug.Log("Icone chargée : " + GetIcon(datas[11]));		//Récupération de l'image en tant qu'icone
	}

	public override void UseSkill()
	{
		base.UseSkill();
	}

	public void CastSpell()
	{
		
	}
}
