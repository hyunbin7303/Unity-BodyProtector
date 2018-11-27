
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Varlab.Database.Domain;

public class PlayerController : NetworkBehaviour
{

    //Another control is the [Command] attribute.
    //The [Command] attribute indicates that the following function will be called by the Client,
    // but will be run on the Server.
    [SyncVar]
    private int Score;

    public GameObject capsule;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Texture2D menuIcon;
    public Text ScoreText;
    public Health health;
    //public Varlab.Database.Domain.Account acc; 

    void Start ()
    {
        health = GetComponent<Health>();
    }

    void Update ()
    {
        /*
         LocalPlayer is part of NetworkBehaviour and all scripts that derive from NetworkBehaviour will 
         understand the concept of a LocalPlayer.
         */
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    // This method is used for assigning color to the main character that player is playing.
    public override void OnStartLocalPlayer()
    {
        capsule.GetComponent<MeshRenderer>().material.color = Color.blue;

        //Account newPlayer = new Account();
        //newPlayer.AccountID = GameManager.instance.playScript.accounts.Count + 1;
        //newPlayer.Username = "KevAustin" + newPlayer.AccountID;
        //newPlayer.PasswordHash = "SOMETHING" + 100 + newPlayer.AccountID;
        //newPlayer.Email = newPlayer.Username + "@Conestogac.on.ca";
        //newPlayer.IsOnline = true;

        //// Check if the Account exists in the database...
        //Account entity = DatabaseManager.instance.accountDAL.GetAccountByID(newPlayer.AccountID);
        //if (entity == null)
        //{
        //    // If the account does not exist, this means that a new player record is inserted
        //    DatabaseManager.instance.accountDAL.CreateAccount(newPlayer);
        //}
        //else
        //{
        //    // Otherwise, we found the account information... save it to this local var
        //    newPlayer = entity;
        //    newPlayer.IsOnline = true;
        //}

        //acc = newPlayer;
        //GameManager.instance.playScript.accounts.Add(acc);
        //Debug.Log("[NetworkManager]: Connection, New Player Added : " + newPlayer.Username);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer)
            return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Detect enemy collision with character.");
            health.currentHealth -= 20;
            Debug.Log("Health value : " + health.currentHealth);
            if (health.currentHealth <= 0)
            {
                Debug.Log("GAME PLAYER DIED.");

                GameManager.instance.playersAlive -= 1;

                //GameManager.instance.playScript.SetPlayerOffline(acc);
                CmdPlayerDie();
            }
        }

        // Collision detection only happens on the server-side...
        // Somehow tell the client that their instance died!
    }


    // Tell the server that player has died...
    [Command]
    public void CmdPlayerDie()
    {
        Destroy(this.gameObject);
    }
}
