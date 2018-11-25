
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    //Another control is the [Command] attribute.
    //The [Command] attribute indicates that the following function will be called by the Client,
   // but will be run on the Server.

    public GameObject capsule;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Texture2D menuIcon;

    void Start () {
    }
	// Update is called once per frame
	void Update () {
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
    }

    public void OnTriggerEnter(Collider other)
    {
        if(isLocalPlayer)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Detect enemy collision");
            }
        }
    }
}
