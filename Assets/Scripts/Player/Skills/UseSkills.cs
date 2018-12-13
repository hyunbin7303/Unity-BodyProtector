using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class UseSkills : NetworkBehaviour
{
    public skill[] allSkills;
    public skill[] PlayerSkills;
    public Texture2D barsBackgroundTexture;
    public GameObject bulletFirePrefab;
    public GameObject bulletWaterPrefab;
    public GameObject SpreadBallPrefab;
    public Transform bulletSpawn;

    private bool IsSkill1On;
    private bool IsSkill2On;
    private bool IsSkill3On;
    private bool IsSkill4On;
    private bool IsSkill5On;
    private bool IsAttackOn;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            PlayerSkills[i].ID = allSkills[i].ID;
            PlayerSkills[i].icon = allSkills[i].icon;
            PlayerSkills[i].skillname = allSkills[i].skillname;
            PlayerSkills[i].cooldown = allSkills[i].cooldown;
            PlayerSkills[i].Description = allSkills[i].Description;
        }
    }

    private void Update()
    {
        // LocalPlayer is part of Network NetworkBehaviour and all scripts that derive from NetworkBehaviour will 
        // understand the concept of a LocalPlayer. This statement basically says: "Do I (the client) have authority over this player"
        if (!isLocalPlayer) {
            return;
        }

        IsSkill1On = Input.GetKeyDown(KeyCode.Alpha1);
        IsSkill2On = Input.GetKeyDown(KeyCode.Alpha2);
        IsSkill3On = Input.GetKeyDown(KeyCode.Alpha3);
        IsAttackOn = Input.GetKeyDown(KeyCode.Space);
        if (IsSkill1On)
        {
            Debug.Log("SKILL 1 is selected");
            UseSpell(PlayerSkills[0].ID);
        }
        if (IsSkill2On)
        {
            Debug.Log("SKILL 2 is selected");
            UseSpell(PlayerSkills[1].ID);
        }
        if (IsSkill3On)
        {
            Debug.Log("SKILL 3 is selected");
            UseSpell(PlayerSkills[2].ID);
        }

        if (IsAttackOn)
        {
            CmdFireBall();
        }
    }

    void OnGUI()
    {
        if (!isLocalPlayer)
        {
            return;
        }

#if UNITY_EDITOR
        Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_1.png", typeof(Texture2D));
        Texture2D t2 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_2.png", typeof(Texture2D));
        Texture2D t3 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_3.png", typeof(Texture2D));
#else
                Texture2D t = Resources.Load<Texture2D>("Assets/Textures/Skills/Elixir_1.png");
                Texture2D t2 = Resources.Load<Texture2D>("Assets/Textures/Skills/Elixir_2.png");
                Texture2D t3 = Resources.Load<Texture2D>("Assets/Textures/Skills/Elixir_3.png");
#endif

        Rect rangeSkill1 = new Rect(60, Screen.height - 80, t.width - 20, t.height - 20);
        Rect rangeSkill2 = new Rect(110, Screen.height - 80, t2.width - 20, t2.height - 20);
        Rect rangeSkill3 = new Rect(160, Screen.height - 80, t3.width - 20, t3.height - 20);
        if (GUI.Button(rangeSkill1, t))
        {
            UseSpell(PlayerSkills[0].ID);
        }
        if (GUI.Button(rangeSkill2, t2))
        {
            UseSpell(PlayerSkills[1].ID);
        }
        if (GUI.Button(rangeSkill3, t3))
        {
            UseSpell(PlayerSkills[2].ID);
        }

        if (rangeSkill1.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 80, 200, 100), barsBackgroundTexture);
            GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 70, 200, 200),
                "SKILL NAME : " + PlayerSkills[0].skillname + "\n" +
                "SKILL DESCRIPTION : " + PlayerSkills[0].Description + "\n" + 
                "SKILL COOLDOWN : " + PlayerSkills[0].cooldown + "\n");
        }
        if (rangeSkill2.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 80, 200, 100), barsBackgroundTexture);
            GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 70, 200, 200),
                "SKILL NAME : " + PlayerSkills[1].skillname + "\n" +
                "SKILL DESCRIPTION : " + PlayerSkills[1].Description + "\n" +
                "SKILL COOLDOWN : " + PlayerSkills[1].cooldown + "\n");
        }
        if (rangeSkill3.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 80, 200, 100), barsBackgroundTexture);
            GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 70, 200, 200),
                "SKILL NAME : " + PlayerSkills[2].skillname + "\n" +
                "SKILL DESCRIPTION : " + PlayerSkills[2].Description + "\n" + 
                "SKILL COOLDOWN : " + PlayerSkills[2].cooldown + "\n");
        }
    }

private void UseSpell(int id)
    {
        switch (id)
        {
            case 1:
                SoundManager.instance.PlaySound("Fire");
                CmdFireBall();
                break;
            case 2:
           //     SoundManager.instance.PlaySound("Water");
                CmdWaterBall();
                break;
            case 3:
                //SoundManager.instance.PlaySound("Spread");
                CmdThrowSkills();
                break;
            case 4:
                Debug.Log("SKill 4 is USED");
                break;
            default:
                Debug.Log("SKILL ERROR");
                break;
        }
    }


    /// <summary>
    /// Used to project a "bullet" spell.
    /// TODO: working on it
    /// </summary>
    [Command]
    private void CmdFireBall()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletFirePrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        //Fetch the NetworkIdentity component of the GameObject
        // and assign the owner to the bullet
        bullet.GetComponent<Bullet>().ownerId = GetComponent<NetworkIdentity>().netId;
        //float destroyTime = bullet.GetComponent<Bullet>().expireRate;
        // Add velocity to the bullet
        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6f;

        //bullet.GetComponent<Bullet>().damage = ;
        // Destroy the bullet after 2 seconds
        //Destroy(bullet, destroyTime);
        NetworkServer.Spawn(bullet);
    }

    [Command]
    private void CmdWaterBall()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = Instantiate(
            bulletWaterPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        bullet.GetComponent<Bullet>().ownerId = GetComponent<NetworkIdentity>().netId;
        NetworkServer.Spawn(bullet);
    }

    [Command]
    private void CmdThrowSkills()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate( SpreadBallPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * 10.0f, ForceMode.Impulse);
        bullet.GetComponent<Bullet>().ownerId = GetComponent<NetworkIdentity>().netId;
        NetworkServer.Spawn(bullet);
    }
}
