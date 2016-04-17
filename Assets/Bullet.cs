using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bullet : NetworkBehaviour
{
    [SyncVar]
    public GameObject owner;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform);
        

        //GameObject[] players = GameObject.FindGameObjectsWithTag("PLAYER");
   
       // foreach (GameObject player in players) {
       //     var combat = player.GetComponent<Combat>();
       //     combat.TakeDamage(1);
       // }
        
    }

    void OnTriggerEnter(Collider collision)
    {
        var hit = collision.gameObject;
        if (owner == hit)
        {
            //print("not hitting owner");
            return;
        }

        if (hit.GetComponent<Bullet>())
        {
            //print("not hitting bullet");
            return;
        }

        /*
        if (hit.tag == "floor")
        {
            print("hitting floor");
        }
        else
        {
            print("hit something not floor");
        }
         * */

        Instantiate(Resources.Load("bulletExplosion"), this.transform.position, this.transform.rotation);

        var hitPlayer = hit.GetComponent<PlayerMovement>();
        if (hitPlayer != null)
        {
            print("hitting other player");
            var combat = hit.GetComponent<Combat>();
            combat.TakeDamage(10);
            Destroy(gameObject);

        }
        BaseCoreScript core = hit.GetComponent<BaseCoreScript>();
        if (core)
        {
            print("hitting core");
            core.ReduceHealth();
            Destroy(gameObject);
        }

    }
}
