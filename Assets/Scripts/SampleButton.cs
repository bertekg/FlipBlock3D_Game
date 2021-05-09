using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour
{
    public Button button;
    public Text text;

    private SelectLevelsPackList levelPackList;
    private int currentIndexLevelsPack, currentIndexLevel;
   
    public void Setup(string textToDisplay, SelectLevelsPackList currentLevelPackList, int indexLevelsPack, int indexLevel)
    {
        text.text = textToDisplay;
        levelPackList = currentLevelPackList;
        currentIndexLevelsPack = indexLevelsPack;
        currentIndexLevel = indexLevel;
    }
    public void ShowLevels()
    {
        GameObject.Find("SwitchPage").GetComponent<SelectLevelsPackList>().SwitchToLevels(currentIndexLevelsPack);
        //levelsPackPanel.
        //bShowThisWindow = true;
    }
    public void ShowLevelsPack()
    {
        GameObject.Find("SwitchPage").GetComponent<SelectLevelsPackList>().SwitchToLevelsPacke();
    }
    public void ShowLevel()
    {
        GameObject.Find("SwitchPage").GetComponent<SelectLevelsPackList>().StartLevel(currentIndexLevelsPack, currentIndexLevel);
    }
}
