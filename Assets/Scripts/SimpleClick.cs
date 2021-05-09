using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleClick : MonoBehaviour {

    // Use this for initialization
    private bool bShowThisWindow = false;
    public Rect windowRect = new Rect(20, 20,300, 150);
    public Text myText;
    void OnGUI()
    {
        if (bShowThisWindow)
        {
            windowRect = GUI.Window(0, new Rect(Screen.width/10, 0, 8 * Screen.width/10, Screen.height / 10), DoMyWindow, "You click: " + myText.text);
        }        
    }
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, (8 * Screen.width / 10) - 20, (Screen.height / 10) - 40), "Close It"))
            bShowThisWindow = false;

    }
    public void ShowWindow()
    {
        bShowThisWindow = true;
    }
}
