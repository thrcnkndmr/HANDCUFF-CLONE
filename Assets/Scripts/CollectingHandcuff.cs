using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingHandcuff : MonoBehaviour
{
    private bool isHcCollected;

    private void Start()
    {
        isHcCollected = false;
    }

    public bool IsHcCollected()
    {
        return isHcCollected;
    }


    public void HcCollected()
    {
        isHcCollected = true;
    }
}