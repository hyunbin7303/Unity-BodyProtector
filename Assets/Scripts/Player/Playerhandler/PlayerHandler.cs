using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public PlayerStats Player;


    [SerializeField]
    private Canvas m_Canvas;
    private bool m_SeeCanvas;


    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            if (m_Canvas)
            {
                m_SeeCanvas = !m_SeeCanvas;
                m_Canvas.gameObject.SetActive(m_SeeCanvas);
            }
        }
    }
}