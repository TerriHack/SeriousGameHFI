using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tablet : MonoBehaviour
{

    public bool tabletOn;

    [Header ("Tablet Y positions" )]
    [SerializeField]
    private float tabletOnPos; 
    [SerializeField]
    private float tabletOffPos;

    [Space]
    [Header("Animation Curves")]
    [SerializeField] 
    private LeanTweenType curveIn;
    [SerializeField] 
    private LeanTweenType curveOut;

    private void Start()
    {
        tabletOn = false;
    }
    
    // On Click function it triggers the tablet
    public void TriggerTablet()
    {
        tabletOn = !tabletOn;

        if (tabletOn) LeanTween.moveLocalY(gameObject, tabletOnPos, 0.5f).setEase(curveIn);
        else LeanTween.moveLocalY(gameObject, tabletOffPos, 0.5f).setEase(curveOut);
    }
}
