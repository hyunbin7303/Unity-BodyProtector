using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHealthScript : MonoBehaviour
{

    public RectTransform healthBar;
    private GameObject maincharacter;
    private float curHealth;

    // Use this for initialization
    void Start()
    {
        maincharacter = this.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {


        if (GameManager.instance.IsGameStart)
        {
            if (maincharacter.GetComponent<PlayerController>().health != null)
            {
                curHealth = maincharacter.GetComponent<PlayerController>().health.currentHealth;
                if (curHealth > 0)
                {
                    healthBar.sizeDelta = new Vector2(curHealth, healthBar.sizeDelta.y);
                }
            }



        }

    }
}
