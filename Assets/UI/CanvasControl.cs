//using System;
//using UnityEngine;

///// <summary>
///// Credit: 
///// </summary>
//[Serializable]
//public class CanvasControl
//{
//    [SerializeField]
//    public Canvas prefab;
//    Canvas m_Canvas;

//    public Canvas canvas { get { return m_Canvas; } }
//\


//    /// <summary>
//    /// Creates an instance of the UI canvas to display.
//    /// </summary>
//    public virtual void Show()
//    {
//        if (prefab == null)
//        {
//            Debug.LogError("[CanvasControl.cs]: Canvas prefab '" + prefab.gameObject.name + "' is null");
//            return;
//        }

//        if (m_Canvas != null)
//            return;

//        m_Canvas = (Canvas)GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
//        //GameObject.DontDestroyOnLoad(m_Canvas.gameObject);
//    }

//    /// <summary>
//    /// Hides the UI canvas from view.
//    /// </summary>
//    public void Hide()
//    {
//        if (m_Canvas == null)
//            return;

//        GameObject.Destroy(m_Canvas.gameObject);
//        m_Canvas = null;
//    }

//    public virtual void OnLevelWasLoaded()
//    {
//    }
//}

//[Serializable]
//public class ScoreCanvasControl : CanvasControl
//{
//    public void Show(string status)
//    {
//        base.Show();

//        //GuiLobbyManager.s_Singleton.offlineCanvas.Hide();

//        // The ScoreCanvasHook is a hook that allows for cust
//        var hooks = canvas.GetComponent<ScoreCanvasHooks>();
//        if (hooks == null)
//            return;

//        hooks.SetCount(0);

//        //

//        //hooks.OnStopHook = OnGUIStop;

//        //hooks.SetAddress(GuiLobbyManager.s_Singleton.networkAddress);
//        //hooks.SetStatus(status);

//        //GuiLobbyManager.s_Singleton.onlineStatus = status;

//        //EventSystem.current.firstSelectedGameObject = hooks.firstButton.gameObject;
//        //EventSystem.current.SetSelectedGameObject(hooks.firstButton.gameObject);
//    }

//    public void OnServerStop()
//    {
//        // The ScoreCanvasHook is a hook that allows for cust
//        var hooks = canvas.GetComponent<ScoreCanvasHooks>();
//        if (hooks == null)
//            return;

//        hooks.SetCount(0);
//        Hide();
//    }

//    public void AllPlayersDied()
//    {
//        Hide();
//    }
//}