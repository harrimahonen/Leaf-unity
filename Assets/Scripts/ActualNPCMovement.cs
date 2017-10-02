using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualNPCMovement : MonoBehaviour {
	public float speed = 1f;
	float clockTimer = 3f;
	float jumpTimer = 1f;
	public float maxJumpTimer = 1f;
	public float jumpHeight = 3f;
	public string wantedMoveType; //Select "WallToWallMove", "TimedMovement", "JumpingMovement" or "FollowPlayer"
	Rigidbody2D rb2D;

	private GameObject wayPoint;
	private Vector3 wayPointPos;

	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		wayPoint = GameObject.Find("WayPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (wantedMoveType.Equals("WallToWallMove")) {
			WallToWallMove();
		} else if (wantedMoveType.Equals("TimedMovement")) {
			TimedMovement();
		} else if (wantedMoveType.Equals("JumpingMovement")) {
			JumpingMovement();
		} else if (wantedMoveType.Equals("FollowPlayer")) {
			FollowPlayer();
		}

	}
	//This determines what happens when our object collides with a wall
	void OnTriggerEnter2D(Collider2D col) {

		switch (col.tag) {

			case "LevelBorder":
				if (wantedMoveType.Equals("WallToWallMove") || wantedMoveType.Equals("TimedMovement") || wantedMoveType.Equals("JumpingMovement")) {
					if (speed > 0) {
						speed = -1f;
					} else if (speed < 0) {
						speed = 1f;
					}
				} else if (wantedMoveType.Equals("FollowPlayer")) {
					rb2D.AddForce (new Vector2 (0f, jumpHeight * 100f));
					rb2D.gravityScale = 1;
				}
			break;

		}
	}

	//Moves left or right until it collides with a wall
	//Colliding with a wall switches movement direction
	void WallToWallMove() {
		if (speed > 0) {
			transform.Translate (speed * Time.deltaTime, 0, 0);
		} else if (speed < 0) {
			transform.Translate (speed * Time.deltaTime, 0, 0);
		}
	}

	//Basic WallToWallMove with a timer
	//Once the timer expires, movement will stop
	void TimedMovement() {
		if (clockTimer > 0) {
			WallToWallMove ();
			clockTimer -= Time.deltaTime;
		}
	}

	//Basic WallToWallMove with jumping added
	//You can set the "jumpHeight" and how often the object jumps with "maxJumpTimer" 
	void JumpingMovement() {
		if (jumpTimer < 0) {
			jumpTimer = maxJumpTimer;
			rb2D.AddForce (new Vector2 (0f, jumpHeight * 100f));
			rb2D.gravityScale = 1;
		} else {
			WallToWallMove ();
		}
		jumpTimer -= Time.deltaTime;
	}

	//Follows the player in simple surface
	//Ability to jump while following next???
	void FollowPlayer() {
		wayPointPos = new Vector3(wayPoint.transform.position.x, transform.position.y, 0);
		transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
	}
}
