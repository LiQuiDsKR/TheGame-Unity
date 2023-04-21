using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {
	bool isDieFlag = false;
	bool isAwake = false;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position) <= 9) {
			isAwake = true;
		}
		if (isAwake) {
			OnEnable ();
		}
			
	}

	void OnEnable() {
		rb.velocity = new Vector2 (-2, rb.velocity.y);

		if (isDieFlag) {
			GameObject.Find("Player").GetComponent<PlayerController> ().GAMEOVER ();
			if (GameObject.Find("Player").GetComponent<PlayerController> ().effectAlpha >= 1.0f) {
				SceneManager.LoadScene (gameObject.scene.name);
				GameObject.Find("Player").GetComponent<PlayerController> ().effectAlpha = 0;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{

		// Ground 태그가 붙은 오브젝트 중에서
		if (collision.collider.tag == ("Player"))
		{
			collision.collider.GetComponent<PlayerController> ().isFreeze = true;
			isDieFlag = true;
		}
	}
}
