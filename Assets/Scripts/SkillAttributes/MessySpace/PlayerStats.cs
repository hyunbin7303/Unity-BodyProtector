using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Messyspace
{
    public class PlayerStats : NetworkBehaviour
    {
        [Header("Main Player Stats")]
        public string PlayerName;


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

        public int PlayerLevel = 1;
        public int PlayerHP = 50;

        [SyncVar(hook = "OnChangeStatus")]
        public CharacterStatus status = CharacterStatus.ALIVE;

        [Header("Player Attributes")]
        public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

        [Header("Player Skills Enabled")]
        public List<Skills> Playerskills = new List<Skills>();

        void Start()
        {
        }

        void Update()
        {
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

        /// <summary>
        /// SyncHook method for sync character status.
        /// Is called when the CharacterStatus is changed.
        /// </summary>
        /// <param name="status"></param>
        void OnChangeStatus(CharacterStatus status)
        {
            DevLog.Log("CharacterState", "Player id <" + GetComponent<NetworkIdentity>().netId + "> status = " + status.ToString());
            this.status = status;
        }
    }
}

