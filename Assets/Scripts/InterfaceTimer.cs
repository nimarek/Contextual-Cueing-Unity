using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceTimer : MonoBehaviour
{
    /*
     * This script displays a timer on the user interface level.
     */
    
    private float currentTime;
    public float startingTime;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
    }
}
