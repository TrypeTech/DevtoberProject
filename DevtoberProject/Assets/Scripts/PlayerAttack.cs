using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    // NOTe: Needed for to work
    // Create a New layer call it Enemy and add your enemies to that layer
    // Create a NewEmpty Object and place it where you want your player to attack
    // add it to the attackPos and make sure it is on the playerObject that has a animator
    // Make sure the player animator attack trigger name is "Attack"
    // Ajust Attack range and you should see it in the editor the range
    // Add this script to the Object that has a animator

    public KeyCode attackButton = KeyCode.A;
    private float timeBtwAttack = 0.3f;
    public float startTimeBtwAttack ;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage = 10;
    Animator anim;

    public float enemyKnockBackStrenght = 4f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(attackButton))
            {
                anim.SetTrigger("Attack");
                Collider[] enemiesToDamage = Physics.OverlapSphere(attackPos.position, attackRange, whatIsEnemies);
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);


                    // Add Force to enemy when hit
                  if (enemiesToDamage[i].gameObject.GetComponent<Rigidbody>() != null && enemiesToDamage[i].gameObject.GetComponent<Enemy>().Invencible == false )
                    {
                        Vector3 direction = enemiesToDamage[i].transform.position - transform.position;
                        direction.y = 0;
                        enemiesToDamage[i].gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * enemyKnockBackStrenght, ForceMode.Impulse);
                    }
                       
                }
            }

            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
      
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
