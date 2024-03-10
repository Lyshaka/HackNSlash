using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
	protected bool activable = false;

	public bool IsActivable()
	{
		return (activable);
	}

	public virtual bool UseObject()
	{
		if (activable)
		{
			activable = false;
			return (true);
		}
		return (false);
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
