using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ProjectileParent
{
	[SerializeField] private float speed = 20f;				//Vitesse de la boule de feu
	private Rigidbody rb;									//Rigidbody de la boule de feu
	private float lifespan = 5f;							//Durée de vie de la boule de feu

	void Start()
	{
		rb = GetComponent<Rigidbody>();						//Récupération du rigidbody
		Destroy(gameObject, lifespan);						//Destruction de la boule de feu en fin de vie
	}

	void Update()
	{
		rb.velocity = gameObject.transform.forward * speed;	//Déplacement de la boule de feu dans la direction de spawn
	}

	void OnTriggerEnter(Collider coll)						//Test du contact du collider
	{
		if (coll.gameObject.layer != 3 && coll.gameObject.layer != 6)		//Test si l'élément touché est différent du joueur (autre que le joueur)
		{
			Enemy enemyScript = coll.gameObject.GetComponent<Enemy>();		//Récupération du script de l'ennemi sur la cible touchée
			if (enemyScript != null)										//Vérification de l'existence du script (aka si la cible touchée est un ennemi)
			{
				SendEffect(enemyScript);									//Si oui on envoie les effets voulus
			}
			StartCoroutine(PlayFeedback());
		}
	}

	void SendEffect(Enemy target)											//Envoi de l'effet à l'ennemi
	{
		target.ApplyEffect(damage);
	}

	IEnumerator PlayFeedback()
	{
		speed = 0;
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);											//Destruction du projectile en cas de collision avec un objet
	}
}
