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

    public ScoreCanvasControl scoreCanvas;

    private bool IsSkill1On;
    private bool IsSkill2On;
    private bool IsSkill3On;
    private bool IsSkill4On;
    private bool IsSkill5On;
    private bool IsAttackOn;
    private void Start()
    {
        scoreCanvas.Show();

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
    }

    private void FixedUpdate()
    {
        if (IsAttackOn)
        {
            SoundManager.instance.PlaySound("Fire");
            CmdFireBall();
        }
    }


    private void OnGUI()
    {
#if UNITY_EDITOR
        Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_1.png", typeof(Texture2D));
        Texture2D t2 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_2.png", typeof(Texture2D));
        Texture2D t3 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_3.png", typeof(Texture2D));
#else
        Texture2D t = Resources.Load<Texture2D>("Assets/Textures/Skills/Elixir_1.png");
        Texture2D t2 = Resources.Load<Texture2D>("Assets/Textures/Skills/Elixir_2.png");
        Texture2D t3= Resources.Load<Texture2D>("Assets/Textures/Skills/Elixir_3.png");

#endif
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
                "SKILL DESCRIPTION : " + PlayerSkills[0].Description + "\n");
        }
        if (rangeSkill2.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 80, 200, 100), barsBackgroundTexture);
            GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 70, 200, 200),
                "SKILL NAME : " + PlayerSkills[1].skillname + "\n" +
                "SKILL DESCRIPTION : " + PlayerSkills[1].Description + "\n");
        }
        if (rangeSkill3.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 80, 200, 100), barsBackgroundTexture);
            GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 70, 200, 200),
                "SKILL NAME : " + PlayerSkills[2].skillname + "\n" +
                "SKILL DESCRIPTION : " + PlayerSkills[2].Description + "\n");
        }

    }

    private void UseSpell(int id)
    {
        switch (id)
        {
            case 1:
                CmdFireBall();
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
    [Command]
    private void CmdFireBall()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 5.0f);
        NetworkServer.Spawn(bullet);
    }




}


