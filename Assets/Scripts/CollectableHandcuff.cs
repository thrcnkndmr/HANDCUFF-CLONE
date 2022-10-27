using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHandcuff : MonoBehaviour
{
    private bool isCollected;
    private int index;


    private void Start()
    {
        isCollected = false;
    }

    public bool IsHcCollected()
    {
        return isCollected;
    }

    public void Collected()
    {
        isCollected = true;
    }
}
