using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestData : MonoBehaviour
{
	public List<Spell> spellList = new List<Spell>();

	// Start is called before the first frame update
	void Start()
	{
		GetData();													//On récupère les données du tableau afin de les ranger dans la liste des spells
	}

	// Update is called once per frame
	void Update()
	{
	
	}

	public void GetData()
	{
		string[] datas = LoadData.ReadString("Skills.tsv");			//On récupère le tableau de string chargé par load data
		for (int i = 1; i < datas.Length; i++)						//On parcours ce tableau (en omettant le premier qui correspond à la première colonne)
		{
			spellList.Add(LoadData.CreateSpellsData(datas[i]));		//On ajoute chaque spell à la liste
			//Debug.Log("Test " + i +" : " + datas[i]);				//Et on l'affiche dans la console pour tester leurs valeurs
		}
	}
}
