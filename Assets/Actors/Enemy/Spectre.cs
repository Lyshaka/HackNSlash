using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectre : Enemy
{
	[SerializeField] private Material phaseMat;
	[SerializeField] private Collider coll;

	[SerializeField] private float phaseTime = 5f;		//Temps de phase
	[SerializeField] private bool phase = true;			//Phase actuelle

	// Start is called before the first frame update
	void Start()
	{
		Initialize();									//Initialisation de l'ennemi
		StartCoroutine(PhaseShift());					//Début de la coroutine qui change la phase
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public override void ApplyEffect(float damage)
	{
		if (phase)														//On applique les dégâts que si la phase est active
		{
			base.ApplyEffect(damage);
		}
	}

	public override void ApplyChannelEffect(float damage, float deltaTime)
	{
		if (phase)														//On applique les dégâts que si la phase est active
		{
			base.ApplyChannelEffect(damage, deltaTime);
		}
	}

	IEnumerator PhaseShift()
	{
		phase = !phase;													//Changement de phase
		coll.enabled = phase;						//Activation/Désactivation du collider en fonction de la phase
		if (phase)
		{
			currentMat = defaultMat;
			renderer.material = currentMat;
			//mat.color = new Color(255f, mat.color.g, mat.color.b, 1f);	//Material quand phase activé
		}
		else
		{
			currentMat = phaseMat;
			renderer.material = currentMat;
			//mat.color = new Color(0f, mat.color.g, mat.color.b, 0.2f);	//Material quand phase désactivé
		}
		yield return new WaitForSeconds(phaseTime);						//On attends le temps de phase
		StartCoroutine(PhaseShift());									//Et on recommence la coroutine
	}
}
