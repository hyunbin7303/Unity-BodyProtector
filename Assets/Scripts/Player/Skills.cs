using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class Skills : NetworkBehaviour
{
    public skill[] allSkills;
    public skill[] PlayerSkills;
    public Texture2D barsBackgroundTexture;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    private bool IsSkill1On;
    private bool IsSkill2On;
    private bool IsSkill3On;
    private bool IsSkill4On;
    private bool IsSkill5On;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            PlayerSkills[i].ID = allSkills[i].ID;
            PlayerSkills[i].icon = allSkills[i].icon;
            PlayerSkills[i].skillname = allSkills[i].skillname;
            PlayerSkills[i].Description = allSkills[i].Description;
        }
    }

    private void Update()
    {
        IsSkill1On = Input.GetKeyDown(KeyCode.Alpha1);
        IsSkill2On = Input.GetKeyDown(KeyCode.Alpha2);
        IsSkill3On = Input.GetKeyDown(KeyCode.Alpha3);
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

    }
    private void OnGUI()
    {
        Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_1.png", typeof(Texture2D));
        Texture2D t2 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_2.png", typeof(Texture2D));
        Texture2D t3 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_3.png", typeof(Texture2D));
        Rect rangeSkill1 = new Rect(100, Screen.height - 100, t.width - 20, t.height - 20);
        Rect rangeSkill2 = new Rect(150, Screen.height - 100, t2.width - 20, t2.height - 20);
        Rect rangeSkill3 = new Rect(200, Screen.height - 100, t3.width - 20, t3.height - 20);
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
                "SKILL ID : " + PlayerSkills[0].ID);
        }
        if (rangeSkill2.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 80, 200, 100), barsBackgroundTexture);
            GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 70, 200, 200),
                "SKILL NAME : " + PlayerSkills[1].skillname + "\n" +
                "SKILL DESCRIPTION : " + PlayerSkills[1].Description + "\n" +
                "SKILL ID : " + PlayerSkills[1].ID);
        }
        if (rangeSkill3.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 80, 200, 100), barsBackgroundTexture);
            GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 70, 200, 200),
                "SKILL NAME : " + PlayerSkills[2].skillname + "\n" +
                "SKILL DESCRIPTION : " + PlayerSkills[2].Description + "\n" +
                "SKILL ID : " + PlayerSkills[2].ID);
        }


    }




    private void UseSpell(int id)
    {
        switch (id)
        {
            case 1:
                FireBall();
                Debug.Log("FAILED TO BUILD");
               
                Debug.Log("SKill 1 is USED");
                break;
            case 2:
                Debug.Log("SKill 2 is USED");
                break;
            case 3:
                Debug.Log("SKill 3 is USED");
                break;
            case 4:
                Debug.Log("SKill 4 is USED");
                break;
            default:
                Debug.Log("SKILL ERROR");
                break;
        }
    }



    private bool GetHealth()
    {

        return true;
    }
    private bool GetMana()
    {

        return true;
    }

    /* Fire Method 
     * Description : Used for firing bullet.
     * Currently working on right now.
     */
  //  [Command]
    private void FireBall()
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
        NetworkServer.Spawn(bullet);
    }




}


