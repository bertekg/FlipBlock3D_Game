﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickRight : MonoBehaviour
{
    public bool clickObject;
    private void OnMouseDown()
    {
        clickObject = true;
    }
    private void OnMouseUp()
    {
        clickObject = false;
    }
}
