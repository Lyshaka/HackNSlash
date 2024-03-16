using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellManager : MonoBehaviour
{
	public GameObject spellObj;

	public GameObject activeChannelObj;

	public float spellDamage;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log("Damage : " + spellDamage);
	}

	public enum Type
	{
		projectile,
		area,
		channel
	}

	public enum Projectile
	{
		fireball,
		other,

	}

	public enum Area
	{
		circle,

	}

	public enum Channel
	{
		cone,
		beam,

	}

	public bool UseSpell(Transform origin, Spell spell, Player player)
	{
		bool ret = false;
		switch (spell.GetSubType())
		{
			case "Projectile":
				LaunchProjectile(origin, spell, player);
				ret = true;
				break;
			case "Area":
				ret = CastArea(origin, spell, player);
				break;
			case "Channel":
				//doothershit
				break;
			default:
				break;
		}
		return (ret);
	}

	public float ComputeDamage(Spell spell, Player.Stats stats, float playerBaseDamage)
	{
		float dmg = 0f;

		dmg += spell.GetDamage();
		dmg += playerBaseDamage;
		dmg += stats.GetStrength() * spell.GetStrength();
		dmg += stats.GetIntelligence() * spell.GetIntelligence();
		dmg += stats.GetDexterity() * spell.GetDexterity();

		spellDamage = dmg;

		return (dmg);
	}

	void LaunchProjectile(Transform origin, Spell spell, Player player)
	{
		GameObject obj = Instantiate(Resources.Load<GameObject>("Spells/" + spell.GetName()), origin.position, origin.rotation);
		obj.GetComponent<ProjectileParent>().SetDamage(ComputeDamage(spell, player.GetStats(), player.GetDamage()));
	}

	bool CastArea(Transform origin, Spell spell, Player player)
	{
		bool hasHit = Physics.Raycast(origin.position, origin.forward, out RaycastHit hit, 10f, player.GetEnemyLayer());
		if (spell.GetName() == "lightning_strike" && hasHit)
		{
			GameObject obj = Instantiate(Resources.Load<GameObject>("Spells/lightning_strike"), hit.point, Quaternion.identity);
			LightningStrike ls = obj.GetComponent<LightningStrike>();
			ls.SetOrigin(origin);
			ls.SetBounceNb(5);
			ls.SetDamage(ComputeDamage(spell, player.GetStats(), player.GetDamage()));
			ls.AddHitTarget(hit.transform.gameObject);
		}
		return (hasHit);
	}

	public void SetChannel(Transform origin, Spell spell, Player player)
	{
		activeChannelObj = Instantiate(Resources.Load<GameObject>("Spells/" + spell.GetName()), origin);
		activeChannelObj.GetComponentInChildren<LaserBeam>().SetDamage(ComputeDamage(spell, player.GetStats(), player.GetDamage()));
	}

	public void RemoveChannel()
	{
		Destroy(activeChannelObj);
		activeChannelObj = null;
	}

	public void ActivateChannel(bool value)
	{
		activeChannelObj.GetComponentInChildren<LaserBeam>().Activate(value);
	}
}
