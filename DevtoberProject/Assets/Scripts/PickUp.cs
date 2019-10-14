using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {


    public enum ItemType
    {
        Coin,
        PowerUp,
        Key,
        WorldKey,
        Health,
    }

    public int Amount = 1;
    public ItemType type;
    public float SpinSpeed = 5f;
    public GameObject ParticalEffect;
    PlayerStats stats;
	// Use this for initialization
	void Start () {
        stats = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.forward * SpinSpeed * Time.deltaTime);
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(ParticalEffect,transform.position, transform.rotation);
            if (type == ItemType.Coin)
            {
                // add a coin
                stats.AddCoins(1);
            }
            if (type == ItemType.PowerUp)
            {

            }
            if (type == ItemType.Key)
            {

            }
            if (type == ItemType.WorldKey)
            {

            }
            if (type == ItemType.Health)
            {
                stats.GainHealth((float)Amount);
            }
            Destroy(gameObject);
        }
    }
}
