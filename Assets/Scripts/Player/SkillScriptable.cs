using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[CreateAssetMenu(menuName = "Body Protector/Player/Create Skill")]
public class SkillScriptable : ScriptableObject
{
    public int ID;
    public string skillname;
    public string Description;
    public float cooldown;
    public int mana;
    public Sprite icon;
    public int LevelNeeded;
    public int XPNeeded;
    public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();


    public void SetValues(GameObject skillDisplayObj, PlayerStats Player)
    {

        if (skillDisplayObj)
        {
            SkillsDisplay sd = skillDisplayObj.GetComponent<SkillsDisplay>();
            sd.skillName.text = name;
            if (sd.skilLDescription)
                sd.skilLDescription.text = Description;
            if (sd.skillIcon)
                sd.skillIcon.sprite = icon;
            if (sd.skillLev)
                sd.skillLev.text = LevelNeeded.ToString();
        }
    }

    public bool EnableSkill(PlayerStats Player)
    {
        List<SkillScriptable>.Enumerator skills = Player.Playerskills.GetEnumerator();
        while (skills.MoveNext())
        {
            var CurrSkill = skills.Current;
            if (CurrSkill.name == name)
            {
                return true;
            }
        }
        return false;
    }

    public bool GetSkill(PlayerStats Player)
    {
        int i = 0;
        List<PlayerAttributes>.Enumerator attributes = AffectedAttributes.GetEnumerator();
        while (attributes.MoveNext())
        {
            List<PlayerAttributes>.Enumerator PlayerAttr = Player.Attributes.GetEnumerator();
            while (PlayerAttr.MoveNext())
            {
                if (attributes.Current.attribute.name.ToString() == PlayerAttr.Current.attribute.name.ToString())
                {
                    //Update the players attributes.
                    PlayerAttr.Current.amount += attributes.Current.amount;
                    // mark that an attribute was updated.
                    i++;

                }
            }
            if (i > 0)
            {
                //reduce the XP from the Player.
                Player.PlayerXP -= this.XPNeeded;
                //add to the list of skills.
                Player.Playerskills.Add(this);
                return true;
            }
        }
        return false;
    }
}