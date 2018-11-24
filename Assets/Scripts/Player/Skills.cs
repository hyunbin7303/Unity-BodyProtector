using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Skills : MonoBehaviour {
    float ab1Timer = 0;
    public skill[] allSkills;
    public skill[] PlayerSkills;
    private bool IsSkill1On;
    private bool IsSkill2On;
    private bool IsSkill3On;
    private bool IsSkill4On;
    private bool IsSkill5On;
    private void Start()
    {
        PlayerSkills[0].ID = allSkills[0].ID;
        PlayerSkills[0].icon = allSkills[0].icon;
        PlayerSkills[0].skillname = allSkills[0].skillname;
        PlayerSkills[0].Description = allSkills[0].Description;


        PlayerSkills[1].ID = allSkills[1].ID;
        PlayerSkills[1].icon = allSkills[1].icon;
        PlayerSkills[1].skillname = allSkills[1].skillname;
        PlayerSkills[1].Description = allSkills[1].Description;



    }


    private void Update()
    {
        IsSkill1On = Input.GetKeyDown(KeyCode.Alpha1);
        IsSkill2On = Input.GetKeyDown(KeyCode.Alpha2);
        IsSkill3On = Input.GetKeyDown(KeyCode.Alpha3);
        IsSkill4On = Input.GetKeyDown(KeyCode.Alpha4);
        IsSkill5On = Input.GetKeyDown(KeyCode.Alpha5);
        if(IsSkill1On)
        {
            Debug.Log("SKILL 1 is selected");
            UseSpell(PlayerSkills[0].ID);
        }
        if(IsSkill2On) 
        {
            Debug.Log("SKILL 2 is selected");
            UseSpell(PlayerSkills[1].ID);
        }
    }
    private void OnGUI()
    {

        
        Texture2D t = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_1.png", typeof(Texture2D));
        GUI.Label(new Rect(10, 40, t.width-20, t.height-20), t);
        Texture2D t2 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_2.png", typeof(Texture2D));
        GUI.Label(new Rect(10, 80, t2.width - 20, t2.height - 20), t2);
        Texture2D t3 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Skills/Elixir_3.png", typeof(Texture2D));
        GUI.Label(new Rect(10, 120, t3.width - 20, t3.height - 20), t3);

    }



    private void UseSpell(int id)
    {
        switch(id)
        {
            case 1:
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
}
