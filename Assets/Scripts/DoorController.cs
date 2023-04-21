using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public Sprite openImg;
	public Sprite closeImg;

	float effectAlpha = 0;
	string nextSceneName;

	bool nextSceneFlag = false;
	// Use this for initialization
	void Start () {
		if (gameObject.scene.name == "MainScene") {
			nextSceneName = "Stage1";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (nextSceneFlag) {
			if (effectAlpha < 1.0f) {
				effectAlpha += 0.02f;
			}
			if (effectAlpha >= 1.0f) {
				SceneManager.LoadScene (nextSceneName);
				effectAlpha = 0;
			}
		}
	}

	void NextStage() {
		effectAlpha += 0.02f;

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
				nextSceneFlag = true;
			}
		}
	}
}
