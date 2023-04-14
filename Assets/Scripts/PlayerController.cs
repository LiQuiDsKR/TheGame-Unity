using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpForce = 5f;
	public int maxJumps = 1; // 멀티점프 기능 추가
	private int jumpCount = 0;
	public bool isJumping = false;
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		float moveX = Input.GetAxis("Horizontal") * moveSpeed;

		// 방향키로 좌, 우 이동
		if (moveX != 0)
		{
			rb.velocity = new Vector2(moveX, rb.velocity.y);
		}

		// 스페이스 바나 터치로 점프
		if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps || Input.touchCount > 0 && jumpCount < maxJumps && !isJumping)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			jumpCount++;
			isJumping = true;
		}
	}

	// 점프가 끝나면 isJumping을 false로 초기화
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			jumpCount = 0;
			isJumping = false;
		}
	}
}
