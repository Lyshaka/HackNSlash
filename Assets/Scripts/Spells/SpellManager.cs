using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		Debug.Log("Damage : " + spellDamage);
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

	public void UseSpell(Transform origin, Type type, Spell spell, Player player)
	{
		switch (type)
		{
			case Type.projectile:
				LaunchProjectile(origin, spell, player);
				break;
			case Type.area:
				//doothershit
				break;
			case Type.channel:
				//doothershit
				break;
			default:
				break;
		}
	}

	float ComputeDamage(Spell spell, Player.Stats stats, float playerBaseDamage)
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
