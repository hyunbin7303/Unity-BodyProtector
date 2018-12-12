using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsDisplay : MonoBehaviour
{
    public SkillScriptable skillScriptable;
    public TMP_Text skillName;
    public Text skilLDescription;
    public Image skillIcon;
    public TMP_Text skillLev;
    public TMP_Text XPNeeded;
    public TMP_Text skillAttribute;
    public TMP_Text skillAttrAmount;
    [SerializeField]
    private PlayerStats m_PlayerHandler;


    void Start()
    {
        m_PlayerHandler = this.GetComponentInParent<PlayerController>().playerStat;
        //listener for the XP change
        m_PlayerHandler.onXPChange += ReactToChange;
        //listener for the Level change
        m_PlayerHandler.onLevelChange += ReactToChange;

        if (skillScriptable)
            skillScriptable.SetValues(this.gameObject, m_PlayerHandler);

        EnableSkills();
    }

    public void EnableSkills()
    {
        //if the player has the skill already, then show it as enabled
        if (m_PlayerHandler && skillScriptable && skillScriptable.EnableSkill(m_PlayerHandler))
        {
            TurnOnSkillIcon();
        }
        //if the player has the skill already, then show it as enabled
        else if (m_PlayerHandler && skillScriptable && skillScriptable.CheckSkills(m_PlayerHandler))
        {
            this.GetComponent<Button>().interactable = true;
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
        }
        else
        {
            TurnOffSkillIcon();
        }
    }

    private void OnEnable()
    {
        EnableSkills();
    }

    public void GetSkill()
    {
        Debug.Log("GET SKILL CLICKED.");
        if (skillScriptable.GetSkill(m_PlayerHandler))
        {
            Debug.Log("ACCEPETABLE FOR GRET SKILL");
            TurnOnSkillIcon();
        }
    }

    //Turn on the Skill Icon - stop it from being clickable and disable the UI elements that make it change colour
    private void TurnOnSkillIcon()
    {
        Debug.Log("TurnOnSkillIcon. Turning on the skill");
        this.GetComponent<Button>().interactable = false;
        this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
        this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
    }

    //Turn off the Skill Icon so it cannot be used - stop it from being clickable and enable the UI elements that make it change colour
    private void TurnOffSkillIcon()
    {
        this.GetComponent<Button>().interactable = false;
        this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
        this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
    }

    //event for when listener is woken
    void ReactToChange()
    {
        EnableSkills();
    }
}