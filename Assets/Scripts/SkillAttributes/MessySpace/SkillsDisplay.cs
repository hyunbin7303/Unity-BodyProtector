using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Messyspace
{
    public class SkillsDisplay : MonoBehaviour {
        public Skills skill;
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
            m_PlayerHandler = this.GetComponentInParent<PlayerHandler>().Player;
            //m_PlayerHandler.onXPChange += React
        }
        public void EnableSkills()
        {
            if (m_PlayerHandler && skill && skill.EnableSkill(m_PlayerHandler))
            {
                //TurnOnSkillIcon();
            }
            //else if(m_PlayerHandler && skill && skill.CheckSkills(m_PlayerHandler))
        }

    }
}

