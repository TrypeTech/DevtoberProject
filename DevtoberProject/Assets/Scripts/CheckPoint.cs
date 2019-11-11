using UnityEngine;

public class CheckPoint : MonoBehaviour
{

   // Create a empty Add this script to it call it check point
   // Put the empty check point where you want your check point to be
   // add a sphere or box collider to the component and set it to be trigger
   // expand the radious of the spawn point radious variable to the size of your trigger box so you can see it  in the scene

    PlayerStats playerData;
    public float spawnPointRadious = 2f;
    // Start is called before the first frame update
    void Start()
    {
        playerData = FindObjectOfType<PlayerStats>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerData.respawnPoint = transform.position;
            Destroy(gameObject, 1f);
        }
    }


    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnPointRadious);
    }
}
