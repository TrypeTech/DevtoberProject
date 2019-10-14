using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {


    public int CoinItems;
    public float MaxPlayerHealth = 100f;
    public float health;

    Movement movement;

	// Use this for initialization
	void Start () {

        health = MaxPlayerHealth;
        movement = FindObjectOfType<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddCoins(int Amount)
    {
        CoinItems = CoinItems + 1;
    }

    public void TakeDamage(float damage)
    {
        movement.animator.SetTrigger("TakeDamage");
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Debug.Log("Player Had Died");
        }
    }

    public void GainHealth(float healthAmount)
    {
        health += healthAmount;

        if(health > MaxPlayerHealth)
        {
            health = MaxPlayerHealth;
        }
    }
}
