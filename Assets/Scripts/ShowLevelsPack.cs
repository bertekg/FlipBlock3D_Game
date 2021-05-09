using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevelsPack : MonoBehaviour
{
    public bool setThisObjectActive = true;

    public void Start()
    {
        gameObject.SetActive(setThisObjectActive);
    }
    public void ShowThisObject()
    {
        gameObject.SetActive(true);
    }
    public void HideThisObject()
    {
        gameObject.SetActive(false);
    }
}
