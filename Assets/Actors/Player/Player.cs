using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector2 movInput;									//Inputs du character
	[SerializeField] private ParticleSystem PS_LevelUp;			//Particule de level up
	[SerializeField] private float speed = 10f;					//Vitesse du character
	[SerializeField] private GameObject fireballObject;			//Prefab de la boule de feu
	[SerializeField] private GameObject playerMesh;				//Object du mesh du character
	[SerializeField] private UserInterfaceManager ui;			//Interface utilisateur
	[SerializeField] private SpellManager spellManager;			//Manager de spell (duh)
	[SerializeField] private Manager manager;					//Manager :s
	private Rigidbody rb;										//Rigidbody du character
	private Camera cam;											//Camera du joueur
	private RaycastHit hit;										//Impact du raycast du curseur
	private LayerMask detectionLayer;							//Layer de détection du curseur

	[Header("Player Stats")]
	[SerializeField] private float baseDamage = 10f;			//Dégâts de base
	[SerializeField] private float damage;						//Dégâts du joueur
	[SerializeField] private float damagePerLevel = 1.5f;		//Dégâts par niveau
	[SerializeField] private float health;						//Points de vie du joueur
	[SerializeField] private float maxHealth = 100;				//Points de vie maximum du joueur
	[SerializeField] private float mana;						//Mana du joueur
	[SerializeField] private float maxMana = 100;				//Mana maximum du joueur
	[SerializeField] private float manaRegen = 10;				//Regeneration de la mana du joueur (mana/s)
	[SerializeField] private int experience;					//Expérience du joueur
	[SerializeField] private int xpRequired = 100;				//Expérience requise jusqu'au prochain niveau
	[SerializeField] private int level = 1;						//Niveau du joueur
	[SerializeField] private Stats stats = new Stats();

	//[SerializeField] private int strength;						//Force du joueur
	//[SerializeField] private int intelligence;					//Intelligence du joueur
	//[SerializeField] private int dexterity;						//Dexterité du joueur

	[Header("Player Attributes")]
	[SerializeField] private int spellIndex = 0;				//Index de la compétence équipée
	[SerializeField] private List<Spell> availableSpells = new List<Spell>();		//Liste des sorts disponibles

	[Header("Player Inventory")]
	[SerializeField] private List<Item> inventory;					//Inventaire du joueur
	[SerializeField] private List<InteractiveObject> nearObjects;	//Objets à proximité
	[SerializeField] private InteractiveObject nearestObject;		//Objet le plus proche

	private GameObject selected;								//Prefab de l'objet de selection
	private bool buttonHeld = false;
	private bool isChanneling = false;
	private float channelTime = 0f;

	public class Stats
	{
		private int strength;									//Force du joueur
		private int intelligence;								//Intelligence du joueur
		private int dexterity;									//Dexterité du joueur

		public Stats GetStats()
		{
			return (this);
		}

		public int GetStrength()
		{
			return (strength);
		}

		public int GetIntelligence()
		{
			return (intelligence);
		}

		public int GetDexterity()
		{
			return (dexterity);
		}

		public void SetStats(int s, int i, int d)
		{
			strength = s;
			intelligence = i;
			dexterity = d;
		}
	}

	public Stats GetStats()
	{
		return (stats);
	}

	public float GetDamage()
	{
		return (damage);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 7)
		{ 
			if (other.gameObject.GetComponent<InteractiveObject>().IsActivable())
			{
				nearObjects.Add(other.gameObject.GetComponent<InteractiveObject>());
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == 7)
		{
			nearObjects.Remove(other.gameObject.GetComponent<InteractiveObject>());
		}
	}

	InteractiveObject GetNearestObject()
	{
		InteractiveObject ret = null;
		float distance = 999999999;
		foreach (InteractiveObject obj in nearObjects)
		{
			if (Vector3.Distance(gameObject.transform.position, obj.gameObject.transform.position) < distance)
			{
				ret = obj;
				distance = Vector3.Distance(gameObject.transform.position, obj.gameObject.transform.position);
			}
		}
		if (ret != null)
		{
			if (selected == null)
			{
				selected = Instantiate(Resources.Load<GameObject>("Selected"), ret.transform.position, Quaternion.identity);
			}
			else
			{
				selected.transform.position = ret.transform.position;
			}
		}
		else
		{
			Destroy(selected);
		}
		return (ret);
	}

	void UpdateStats()
	{
		int s = 0;
		int i = 0;
		int d = 0;
		foreach (Item item in inventory)
		{
			s += item.GetStrength();
			i += item.GetIntelligence();
			d += item.GetDexterity();
		}
		stats.SetStats(s, i, d);
		damage = baseDamage + (damagePerLevel * level);
		ui.UpdateStatistics(stats, (int)damage);
	}
	
	public void AddExperience(int xp)
	{
		while ((experience + xp) >= xpRequired)
		{
			xp -= xpRequired;
			level++;
			PS_LevelUp.Play();
			xpRequired = 100 + (level * 5);
		}
		experience += xp;
		ui.UpdateExperience(experience, xpRequired, level);
		UpdateStats();
	}

	private void Initialize()
	{
		health = maxHealth;
		ui.UpdateHealth(health, maxHealth);
		mana = maxMana;
		ui.UpdateMana(mana, maxMana);
	}

	void Start()
	{
		rb = GetComponent<Rigidbody>();									//Récupération du rigidbody du character
		cam = GetComponentInChildren<Camera>();							//Récupération de la caméra
		cam.transform.LookAt(gameObject.transform);						//Rotation de la caméra vers le joueur
		detectionLayer = (1 << 6);
		ui = GetComponentInChildren<UserInterfaceManager>();
		Initialize();
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		ui.UpdateExperience(experience, xpRequired, level);
		UpdateStats();
		for (int i = 0; i < 3; i++)
		{
			availableSpells.Add(LoadData.CreateSpellsData(manager.GetSpell(i)));
		}
		ui.UpdateSpell(availableSpells);
	}

	void Update()
	{
		movInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;			//Récupération des inputs du joueur

		nearestObject = GetNearestObject();																			//Calcul de l'objet le plus proche du joueur

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (nearestObject != null && nearestObject.IsActivable())
			{
				if (nearestObject.GetType() == typeof(Chest))
				{
					Debug.Log("You have opened " + nearestObject.gameObject.name);
					Chest chest = nearestObject as Chest;
					inventory.Add(chest.ChestUseObject());
					UpdateStats();
					nearObjects.Remove(nearestObject.GetComponent<InteractiveObject>());
				}
				else
				{
					Debug.Log("Nope");
				}
			}
			else
			{
				Debug.Log("Aucun objet à proximité !");
			}
		}
		

		if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, detectionLayer))	//Lancement de rayon depuis la position du curseur dans le monde
		{
			playerMesh.transform.LookAt(new Vector3(hit.point.x, playerMesh.transform.position.y, hit.point.z));	//Rotation du personnage dans la dierction du point visé par le curseur
		}

		//if (Input.GetButtonDown("Fire1"))																			//Lors de l'appui de la touche de fire on envoie une boule de feu
		//{
		//	GameObject fireball = Instantiate(fireballObject, playerMesh.transform.position, playerMesh.transform.rotation);	//Instanciation de la boule de feu au niveau du jouer dans la direction dans laquelle il regarde
			
		//}

		if (Input.GetButton("Fire1"))
		{
			Debug.Log("Cost : " + availableSpells[spellIndex].GetCost());
			
			if (availableSpells[spellIndex].GetSubType() != "Channel" && !buttonHeld)
			{
				if (mana >= availableSpells[spellIndex].GetCost())
				{
					spellManager.UseSpell(playerMesh.transform, SpellManager.Type.projectile, (Spell)availableSpells[spellIndex], this);
					mana -= availableSpells[spellIndex].GetCost();
					ui.UpdateMana(mana, maxMana);
				}
			}
			buttonHeld = true;
			if (availableSpells[spellIndex].GetSubType() == "Channel")
			{
				if (mana > 0)
				{
					isChanneling = true;
					spellManager.ActivateChannel(true);
					mana -= availableSpells[spellIndex].GetCost() * Time.deltaTime;
					channelTime += Time.deltaTime;
				}
				else
				{
					spellManager.ActivateChannel(false);
				}
			}
		}
		else
		{
			buttonHeld = false;
			if (availableSpells[spellIndex].GetSubType() == "Channel")
			{
				isChanneling = false;
				spellManager.ActivateChannel(false);
			}
			channelTime = 0;
		}

		if (Input.mouseScrollDelta.y < 0f)
		{
			if (spellIndex == 0)
			{
				spellIndex = 2;
			}
			else
			{
				spellIndex--;
			}
			if (availableSpells[spellIndex].GetSubType() == "Channel")
			{
				spellManager.SetChannel(playerMesh.transform, availableSpells[spellIndex], this);
			}
			else
			{
				spellManager.RemoveChannel();
			}
			ui.UpdateSelectedSpell(spellIndex);
		}
		if (Input.mouseScrollDelta.y > 0f)
		{
			if (spellIndex == 2)
			{
				spellIndex = 0;
			}
			else
			{
				spellIndex++;
			}
			if (availableSpells[spellIndex].GetSubType() == "Channel")
			{
				spellManager.SetChannel(playerMesh.transform, availableSpells[spellIndex], this);
			}
			else
			{
				spellManager.RemoveChannel();
			}
			ui.UpdateSelectedSpell(spellIndex);
		}

		rb.velocity = new Vector3(movInput.x, 0f, movInput.y) * speed;												//Application des mouvements sur le personnage
	
		if (mana < maxMana && !isChanneling)
		{
			mana += manaRegen * Time.deltaTime;
			if (mana > maxMana)
			{
				mana = maxMana;
			}
		}
		ui.UpdateMana(mana, maxMana);
	}
}
