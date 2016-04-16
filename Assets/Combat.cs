using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Combat : NetworkBehaviour
{
    public const int maxHealth = 100;

    [SyncVar]
    public int health = maxHealth;
    GameObject healthText;
    GameObject spawnPoint;

    // Use this for initialization
    void Start()
    {
        healthText = GameObject.Find("healthText");
        spawnPoint = GameObject.Find("spawn");
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        health -= amount;
        if (health <= 0)
        {
            // called on the server, will be invoked on the clients
            RpcRespawn();

            health = 100;
        }

        if (isLocalPlayer)
        {
            healthText.GetComponent<Text>().text = "HEALTH " + health;
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to spawn point
            transform.position = spawnPoint.transform.position;
        }
    }
}
