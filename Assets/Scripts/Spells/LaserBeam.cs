using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : ChannelParent
{

	public LineRenderer beamLine;
	public Collider beamColl;
	public List<GameObject> enemies = new List<GameObject>(); //SerializeField causes some errors :s


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			if (!enemies.Contains(other.gameObject.transform.parent.gameObject))
			{
				enemies.Add(other.gameObject.transform.parent.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (enemies.Contains(other.gameObject.transform.parent.gameObject))
		{
			DeleteFromList(other.gameObject.transform.parent.gameObject);
		}
	}

	public void DeleteFromList(GameObject obj)
	{
		if (obj != null && enemies.Contains(obj))
		{
			enemies.Remove(obj);
		}
	}

	public void SetDamage(float value)
	{
		damage = value;
	}

    // Start is called before the first frame update
    void Start()
    {
		beamLine = GetComponent<LineRenderer>();
		beamColl = GetComponent<Collider>();
		Activate(true);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemies)
		{
			if (enemy != null)
			{
				enemy.GetComponent<Enemy>().ApplyChannelEffect(damage, Time.deltaTime);
			}
		}
    }

	public void Activate(bool value)
	{
		beamLine.enabled = value;
		beamColl.enabled = value;
	}
}
