using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    public Vector2 directionalInput;
    Player player;
    public  static bool isAlive = true;

    void Start()
    {

        player = GetComponent<Player>();
    }

    void Update()
    {
        if (isAlive)
        {
            directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.OnJumpInputDown();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                player.OnJumpInputUp();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {

                AttackCube.Attack();
                if (AttackCube.isAttacking())
                {
                    player.Attack();
                }
            }
        }
    }
}