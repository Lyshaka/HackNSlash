using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
	List<Sprite> sprite = new List<Sprite>();
	public Image image;

	// Start is called before the first frame update
	void Start()
	{
		sprite.Add(Resources.Load<Sprite>("1"));
		sprite.Add(Resources.Load<Sprite>("2"));
		sprite.Add(Resources.Load<Sprite>("3"));
		sprite.Add(Resources.Load<Sprite>("4"));
		sprite.Add(Resources.Load<Sprite>("5"));
		foreach (Sprite s in sprite)
		{
			Debug.Log("Sprite : [" + s + "]");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			int r = Random.Range(0, 5);
			Im t = new Im((r + 1).ToString());
			image.sprite = t.s;
			Debug.Log("value : " + (r + 1));
		}
	}

	public class Im
	{
		public Sprite s;

		public Im(string path)
		{
			s = Resources.Load<Sprite>(path);
			Debug.Log("Sprite : [" + s + "]");
		}
	}
}
