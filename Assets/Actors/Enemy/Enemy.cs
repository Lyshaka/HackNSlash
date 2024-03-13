using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header("Enemy stats")]
	[SerializeField] protected float damage = 2;//Dégâts de l'ennemi
	[SerializeField] protected float health;	//Points de vie
	[SerializeField] protected float healthMax;	//Points de vie maximum
	[SerializeField] protected float mana;		//Mana
	[SerializeField] protected float manaMax;	//Mana maximum
	[SerializeField] protected float armor;		//Armor
	[SerializeField] protected int experience = 20;	//Experience

	private Player player = null;

	protected Material mat;
	protected HealthBarManager healthBar;

	// Start is called before the first frame update
	void Start()
	{
		Initialize();							//Initialisation
	}

	// Update is called once per frame
	void Update()
	{
		if (player != null)
		{
			player.ApplyDamage(damage * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 3)
		{
			player = other.gameObject.GetComponent<Player>();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == 3)
		{
			player = null;
		}
	}

	protected void Initialize()
	{
		health = healthMax;						//Set la vie à son maximum
		mana = manaMax;							//Set le mana à son maximum
		if (armor >= 0.99f)						//Clamp des valeurs de l'armure afin de rester dans des valeurs raisonnables
		{
			armor = 0.99f;
		}
		else if (armor < 0)
		{
			armor = 0;
		}
		mat = GetComponent<Renderer>().material;
		healthBar = GetComponentInChildren<HealthBarManager>();
		healthBar.SetPercent(health / healthMax);
	}

	public virtual void ApplyEffect(float damage)	//Virtual -> les enfants peuvent modifier cette méthode
	{
		float appliedDamage = damage * (1 - armor);
		health -= appliedDamage;			//Application des dégâts de à l'ennemi
		GameObject obj = Instantiate(Resources.Load<GameObject>("Number"), transform.position + new Vector3(0f, 3f, 0f), Quaternion.identity);
		obj.GetComponent<NumberManager>().SetNumber(appliedDamage);
		healthBar.UpdateHealthBar(health, healthMax);
		StartCoroutine(Blink());				//Scintillement de l'ennemi
		if (health <= 0)						//Test si l'ennemi a 0 hp ou moins
		{
			GameObject player = GameObject.Find("PlayerCharacter");
			player.GetComponent<Player>().AddExperience(experience);	//On donne l'experience au joueur
			player.GetComponentInChildren<EnemyNavManager>().DeleteFromList(gameObject);	//On détruit l'ennemi en question du script de gestion de leur navigation
			Destroy(gameObject);				//Et on le détruit
		}
	}

	public virtual void ApplyChannelEffect(float damage, float deltaTime)
	{
		float appliedDamage = damage * (1 - armor);
		health -= appliedDamage * deltaTime;
		//Debug.Log("Damage : " + appliedDamage + ", Real Damage : " + appliedDamage * deltaTime);
		healthBar.UpdateHealthBar(health, healthMax);
		if (health <= 0)						//Test si l'ennemi a 0 hp ou moins
		{
			GameObject player = GameObject.Find("PlayerCharacter");
			player.GetComponent<Player>().AddExperience(experience);	//On donne l'experience au joueur
			player.GetComponentInChildren<EnemyNavManager>().DeleteFromList(gameObject);	//On détruit l'ennemi en question du script de gestion de leur navigation
			healthBar.UpdateHealthBar(health, healthMax);
			Destroy(gameObject);				//Et on le détruit
		}
	}

	IEnumerator Blink()
	{
		Color color = mat.color;
		mat.color = Color.white;
		yield return new WaitForSeconds(0.03f);
		mat.color = color;
	}
}
