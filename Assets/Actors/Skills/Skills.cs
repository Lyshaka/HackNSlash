using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class Skills	//Classe abstraite (ne peut pas être instanciée) des compétences
{
	[SerializeReference] protected int id;				//ID du sort
	[SerializeReference] protected string name;			//Nom du sort
	[SerializeReference] protected string type;			//Type du sort
	[SerializeReference] protected string subType;		//Sous-type du sort
	[SerializeReference] protected int levelMax;		//Niveau maximum du sort
	[SerializeReference] protected float damage;		//Dégâts du sort
	[SerializeReference] protected float strength;		//Scaling sur la force
	[SerializeReference] protected float intelligence;	//Scaling sur l'intelligence
	[SerializeReference] protected float dexterity;		//Scaling sur la dextérité
	[SerializeReference] protected float cost;			//Cout du sort (en mana)
	[SerializeReference] protected float castTime;		//Temps de cast
	[SerializeReference] protected Sprite icon;			//Icone du sort
	[SerializeReference] protected int level;			//Niveau du sort

	public string GetName()
	{
		return (name);
	}

	public string GetSubType()
	{
		return (subType);
	}

	public float GetDamage()
	{
		return (damage);
	}

	public float GetStrength()
	{
		return (strength);
	}

	public float GetIntelligence()
	{
		return (intelligence);
	}

	public float GetDexterity()
	{
		return (dexterity);
	}

	public float GetCost()
	{
		return (cost);
	}

	public Sprite GetImage()
	{
		return (icon);
	}

	protected virtual bool GetIcon(string path)
	{
		icon = Resources.Load<Sprite>(path);
		Debug.Log("Path : [" + path + "]");
		Debug.Log("Icon : [" + icon + "]");

		if (icon == null)
		{
			return (false);
		}
		return (true);
	}

	public virtual void UseSkill()
	{

	}

	public bool AddLevel()		//Méthode d'ajout de niveau
	{
		if (level < levelMax)	//On ajout un niveau que si l'on n'a pas atteint le niveau maximum
		{
			level++;
			return (true);		//Auquel cas on retourne true
		}
		return (false);			//Sinon on retourne false
	}

	public bool SubLevel()		//Méthode de soustraction de niveau
	{
		if (level > 0)			//On retire un niveau que si l'on n'est au dessus du niveau minimum (0)
		{
			level--;
			return (true);		//Auquel cas on retourne true
		}
		return (false);			//Sinon on retourne false
	}
}
