using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        this.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        //GameObject[] players = GameObject.FindGameObjectsWithTag("PLAYER");
   
       // foreach (GameObject player in players) {
       //     var combat = player.GetComponent<Combat>();
       //     combat.TakeDamage(1);
       // }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var hitPlayer = hit.GetComponent<PlayerMovement>();
        if (hitPlayer != null)
        {
            var combat = hit.GetComponent<Combat>();
            combat.TakeDamage(10);

            Destroy(gameObject);
        }
    }
}
