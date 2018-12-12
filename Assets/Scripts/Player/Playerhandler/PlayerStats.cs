using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerStats : NetworkBehaviour
{
    [Header("Main Player Stats")]
    public string PlayerName;

    public int PlayerLevel = 1;
    public int PlayerPassiveSkill;
    public int PlayerXP_Full = 100;
    public int CurrentXP = 0;

    public float Speed;
    public float Damage = 30;

    [SerializeField]
    private int m_PlayerXP = 0;
    public int PlayerXP
    {
        get { return m_PlayerXP; }
        set
        {
            m_PlayerXP = value;
            if (onXPChange != null)
                onXPChange();
        }
    }

    [Header("Player Attributes")]
    public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

    [Header("Player Skills Enabled")]
    public List<SkillScriptable> Playerskills = new List<SkillScriptable>();

    public void IncreaseXP(int xpAmount)
    {
        CurrentXP += xpAmount;
        if (CurrentXP >= PlayerXP_Full)
        {
            PlayerLevel++;
            PlayerXP_Full += 100;
            CurrentXP = 0;
        }
        Debug.Log("Current XP Report : " + CurrentXP);
    }
    public delegate void OnXPChange();
    public event OnXPChange onXPChange;
    public delegate void OnLevelChange();
    public event OnLevelChange onLevelChange;

    public void UpdateLevel(int amount)
    {
        PlayerLevel += amount;
    }
    public void UpdateXP(int amount)
    {
        PlayerXP += amount;
    }
}
