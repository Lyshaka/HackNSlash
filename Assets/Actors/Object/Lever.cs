using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractiveObject
{

	public override bool UseObject()
	{
		bool ret = base.UseObject();
		if (ret)
		{
			GetComponent<ActivateLever>().TriggerLever();
		}
		return (ret);
	}

	// Start is called before the first frame update
	void Start()
    {
        activable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
