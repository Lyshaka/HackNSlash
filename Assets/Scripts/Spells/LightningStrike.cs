using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{

	private int bounces;
	private float damage;
	private Transform origin;
	[SerializeField] private List<GameObject> hitTargets = new List<GameObject>();
	private LineRenderer line;
	private LayerMask enemyLayer;

	public void SetOrigin(Transform value)
	{
		origin = value;
	}

	public void SetBounceNb(int nb)
	{
		bounces = nb;
	}

	public void SetDamage(float value)
	{
		damage = value;
	}

	public void AddHitTarget(GameObject value)
	{
		hitTargets.Add(value);
	}

	public void SetHitTargets(List<GameObject> targets)
	{
		hitTargets = targets;
	}

	void SetLine(Vector3 a, Vector3 b)
	{
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, a);
		line.SetPosition(1, b);
	}

	GameObject GetValidTarget()
	{
		GameObject ret = null;
		List<GameObject> validTargets = new List<GameObject>();
		Collider[] targets = Physics.OverlapSphere(transform.position, 5f, enemyLayer);
		foreach (Collider target in targets)
		{
			if (!hitTargets.Contains(target.gameObject) && target.gameObject.layer == 8)
			{
				validTargets.Add(target.gameObject);
			}
		}
		if (validTargets.Count > 0)
		{
			ret = validTargets[Random.Range(0, validTargets.Count)];
			hitTargets.Add(ret);
		}
		//Debug.Log("Test : " + ret);
		return (ret);
	}

	void DealDamage()
	{
		Collider[] targets = Physics.OverlapSphere(transform.position, 1f, enemyLayer);
		foreach (Collider target in targets)
		{
			target.gameObject.GetComponent<Enemy>().ApplyEffect(damage);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		enemyLayer = (1 << 8);
		DealDamage();
		StartCoroutine(Bounce());
		Destroy(gameObject, 0.2f);
	}

	// Update is called once per frame
	void Update()
	{
		if (origin != null)
		{
			SetLine(origin.position, transform.position);
		}
	}

	IEnumerator Bounce()
	{
		yield return new WaitForSeconds(0.1f);
		GameObject target = GetValidTarget();
		if (bounces > 0 && target != null)
		{
			GameObject obj = Instantiate(Resources.Load<GameObject>("Spells/lightning_strike"), target.transform.position, Quaternion.identity);
			LightningStrike ls = obj.GetComponent<LightningStrike>();
			ls.SetOrigin(gameObject.transform);
			ls.SetBounceNb(bounces - 1);
			ls.SetDamage(damage);
			ls.SetHitTargets(hitTargets);
		}
	}
}
