using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCube : MonoBehaviour {

    //määrittää mitä seurataan
    public Transform player;

    public float attackDistance = 1;
    public float attackTime;
    public float attackCooldown;

    private Vector2 lastPos;
    private Vector2 newPos;

    private float direction;
    private float aDirection;

    private static float attackTimer;
    private static float cooldownTimer;

    public static bool attacking = false;
    private static bool cooldown = false;
    private static BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        lastPos = player.position;
        collider.enabled = false;

    }

	public static bool isAttacking()
	{
		return attacking;
	}
    public static void Attack ()
    {
        if (!cooldown)
        {
            Debug.Log("attacked");
            collider.enabled = true;
            attacking = true;            
            cooldown = true;
        }
	}
    private void Update()
    {

        direction = Input.GetAxisRaw("Horizontal");

        Vector2 playerPos = Vector2.MoveTowards(transform.position, player.position, 1000);

        if(direction!=0)
        {
            aDirection = direction;
        }
        newPos.x = aDirection*attackDistance + playerPos.x;
        newPos.y = playerPos.y;

        transform.position = newPos;
        lastPos = player.position;

        
    }

    private void FixedUpdate()
    {
        if (attacking)
        {
            attackTimer += 1.0F * Time.deltaTime;
        }


        if (attackTimer >= attackTime)
        {
            
            collider.enabled = false;
            attackTimer = 0;
        }

        if (cooldown)
        {
            attacking = false;
            cooldownTimer += 1.0F * Time.deltaTime;
        }

        if (cooldownTimer >= attackCooldown)
        {
            cooldown = false;
            cooldownTimer = 0;
        }
    }
}
