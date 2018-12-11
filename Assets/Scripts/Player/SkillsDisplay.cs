using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsDisplay : MonoBehaviour {
    public UseSkills skill;
    public Text skillName;
    public Text skilLDescription;
    public Image skillIcon;
    public Text skillLev;
    public Text XPneeded;
    public Text skillAttribute;
    public Text skillAttrAmount;
    [SerializeField]
    private PlayerStats m_PlayerHandler;

    private void Start()
    {
        m_PlayerHandler = this.GetComponentInParent<PlayerController>().playerStat;
    }




}