
using UnityEngine;
using UnityEngine.Networking;
public class PlayerController : NetworkBehaviour
{
    public GameObject capsule;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
	}
    public override void OnStartLocalPlayer()
    {
        capsule.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}
