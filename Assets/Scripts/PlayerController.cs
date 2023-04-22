using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpForce = 5f;
	public int maxJumps; // 멀티점프 기능 추가
	private int jumpCount;
	private Rigidbody2D rb;
	public bool hasKey = false;
	public bool isFreeze = false;

	public float effectAlpha = 0.0f;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		jumpCount = maxJumps;
	}

	void Update() {
		// CameraMove ();
		PlayerEscapeScreen ();
		if (!isFreeze) {
			// 좌우방향키 이동
			float moveX = Input.GetAxis("Horizontal") * moveSpeed;
			rb.velocity = new Vector2 (moveX, rb.velocity.y);

			// 스페이스 바로 점프
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (jumpCount > 0) {
					jumpCount--;
					rb.velocity = new Vector2(rb.velocity.x, jumpForce);
				}
			}

		}



	}

	void PlayerEscapeScreen() {
		if (GameObject.Find ("Player").transform.position.y <= -10) {
			GAMEOVER ();
			if (effectAlpha >= 1.0f) {
				SceneManager.LoadScene (gameObject.scene.name);
				effectAlpha = 0;
			}
		}
	}

	public void GAMEOVER() {
		effectAlpha += 0.02f;
		GameObject.Find ("GameOverEffect").GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, effectAlpha);
	}

	void CameraMove() {
		if (Camera.main.transform.position.x >= gameObject.transform.position.x + 2) {
			Camera.main.transform.position = new Vector3 (gameObject.transform.position.x + 2, Camera.main.transform.position.y, Camera.main.transform.position.z);
		}
		if (Camera.main.transform.position.x <= gameObject.transform.position.x - 2) {
			Camera.main.transform.position = new Vector3 (gameObject.transform.position.x - 2, Camera.main.transform.position.y, Camera.main.transform.position.z);
		}
	}

	// 점프가 끝나면 isJumping을 false로 초기화
	void OnCollisionEnter2D(Collision2D collision) {
		
		// Ground 태그가 붙은 오브젝트 중에서
		if (collision.collider.tag == ("Ground")) {
			print ("asdf");
			// BoxCast를 이용하여 지정한 위치에서 충돌 체크
			RaycastHit2D hit = Physics2D.BoxCast(gameObject.transform.position + new Vector3(0, -0.41f, 0), new Vector2(0.4f,0.4f),0,Vector2.down, 1, LayerMask.GetMask("GroundLayer"));
			print (hit.collider.gameObject.name);

			// 충돌한 오브젝트가 Ground 태그가 붙은 오브젝트이고, 충돌한 지점이 Player 오브젝트의 아래쪽이라면
			if (hit.collider != null && hit.collider.tag == ("Ground"))	{
				jumpCount = 0;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		jumpCount = maxJumps;
	}

}
