using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour
{
    [Header("Main Player Stats")]
    public string PlayerName;

    public int PlayerLevel = 1;
    public int PlayerPassiveSkill;

    public float Speed = 3;
    public float Damage = 30;
    [SerializeField]
    private int m_PlayerXP = 0;
    [SerializeField]
    private Canvas m_InfoCanvas;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private TMP_Text DamageText;
    [SerializeField]
    private TMP_Text SpeedText;


    public int PlayerXP_Full = 100;
    public int CurrentXP = 0;
    public RectTransform ExBar;
    public Slider hudExBar;
    public TMP_Text ExText;


    private bool m_SeeInfoCanvas = false;
    private void Start()
    {
        if(hudExBar != null)
        {
            hudExBar.value = CalculateEx();
        }
        levelText.text = PlayerLevel.ToString();
        DamageText.text = Damage.ToString();
        SpeedText.text = Speed.ToString();
        m_InfoCanvas.gameObject.SetActive(m_SeeInfoCanvas); // don't show the info level up canvas
    }

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
            levelText.text = PlayerLevel.ToString();
            Debug.Log("POP UP TEST");
            m_InfoCanvas.gameObject.SetActive(true);
            StartCoroutine(RemoveAfterSeconds(3));
            //RemoveAfterSeconds(3, m_InfoCanvas.gameObject);
            PlayerXP_Full += 100;
            CurrentXP = 0;
        }
        Debug.Log("Current XP Report : " + CurrentXP);
    }

    public void IncreaseDamage(float damage)
    {
        Debug.Log("INCREASE DAMAGE METHOD IS CALLED. : " + damage);
        Damage += damage;
        DamageText.text = Damage.ToString();    // Display to screen.
    }
    public void IncreaseSpeed(float speed)
    {
        Debug.Log("INCREASE SPEED METHOD IS CALLED. : " + speed);
        Speed += speed;
        SpeedText.text = Speed.ToString();
    }
    public void IncreaseValue(float value)
    {
        Debug.Log("INCREASE VALUE IS CALLED. : " + value);
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


    public IEnumerator RemoveAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        m_InfoCanvas.gameObject.SetActive(false);
    }

    void onChangeEx(int experience)
    {
        ExBar.sizeDelta = new Vector2(experience, ExBar.sizeDelta.y);
        if(ExBar != null)
        {
            hudExBar.value = experience / PlayerXP_Full;
            ExText.text = experience.ToString("F0");
        }
    }
    float CalculateEx()
    {
        return CurrentXP / PlayerXP_Full;
    }


}
