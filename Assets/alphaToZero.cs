using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alphaToZero : MonoBehaviour
{
    public Image ToReset;
    public void ResetImage()
    {
        ToReset.color = new Color(0,0,0,0);
    }
}
