using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	
	public int HealthPoints = 2;
    public int MaxHp = 2;
	public bool Friendly;
	public string Weapon;
	public bool Interactable;
    public int spawnLength = 3;
    public Vector3 spawnPoint;
    Character obj;
    GameObject pc;

	// Use this for initialization
	void Start () {
        pc = GameObject.Find("PlayerCharacter");
        if (pc)
        {
            spawnPoint = new Vector3(16, -5, 0);
            pc.transform.position = spawnPoint;
        }
	}

    // Update is called once per frame

	void Update () {
		if (HealthPoints <= 0) {
            //die();
		}
	}

	public void changeHP(int howMuch) {
		if (HealthPoints + howMuch <= MaxHp) {
			HealthPoints += howMuch;
		} else if (HealthPoints + howMuch > MaxHp) {
			HealthPoints = MaxHp;
		}
	}

	public void changeMaxHp(int howMuch) {
		MaxHp += howMuch;
		if (MaxHp < HealthPoints) {
			HealthPoints = MaxHp;
		}
	}

    public void npcDie(Character obj)
    {
        Destroy(obj, 1f);
    }

    public void playerDie()
    {
        PlayerInput.isAlive = false;
        Invoke("respawn", spawnLength);
        changeHP(MaxHp);
    }
    /* public void respawn()
    {
        transform.position = spawnPoint.position;
    }
    */
}
