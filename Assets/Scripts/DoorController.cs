using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public Sprite openImg;
	public Sprite closeImg;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider)
	{

		// Ground 태그가 붙은 오브젝트 중에서
		if (collider.tag == ("Player"))
		{
			if (GameObject.Find ("Player").GetComponent<PlayerController> ().hasKey == true) {
				GameObject.Find ("Player").GetComponent<PlayerController> ().hasKey = false;
				gameObject.GetComponent<SpriteRenderer> ().sprite = openImg;
				print ("stage clear!");
			}
		}
	}
}
