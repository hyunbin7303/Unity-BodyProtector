using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class skill {
    public Texture2D icon;
    public int ID;
    public string skillname;
    public string Description;
    public float cooldown;
    public skill(skill s)
    {
        ID = s.ID;
        icon = s.icon;
        skillname = s.skillname;
        Description = s.Description;
        cooldown = s.cooldown;
    }

}
