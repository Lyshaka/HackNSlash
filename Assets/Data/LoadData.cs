using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LoadData
{
	//[MenuItem("Tools/Load_Data/Skills")]				//On crée le bouton spells dans le menu tools pour accéder rapidement au chargement des données
	static public string[] ReadString(string file)
	{
		string path = "Assets/Data/" + file;			//On précise le chemin d'accès de notre ficher
		StreamReader reader = new StreamReader(path);	//On ouvre notre fichier en lecture
		string data = reader.ReadToEnd();				//On copie le contenu du fichier dans la variable data
		reader.Close();									//On ferme notre fichier
		string[] datas = data.Split('\n');				//On explose la string complète en un tableau, en utilisant le retour à la ligne en tant que caractère de séparation

		//Debug.Log("Done !");

		return (datas);									//On retourne notre tableau de data
	}

	static public Spell CreateSpellsData(string data)	//Méthode de création d'un spell à partir des données
	{
		string[] datas = data.Split('\t');				//Explosion de la chaîne en utilisant le tab en caractère de séparation
		Spell spell = new Spell(datas);					//Construction du spell depuis son constructeur
		
		return (spell);									//Retour du spell créé
	}

	static public Item CreateItemData(string data)
	{
		string[] datas = data.Split('\t');
		Item item = new Item(datas);

		return (item);
	}

}
