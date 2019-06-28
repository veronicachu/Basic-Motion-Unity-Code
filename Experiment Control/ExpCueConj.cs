﻿/// <summary>
/// This code will: 
/// 1) Randomly select one of the trials from the list generated by ExpSetup.m script
/// 2) Take the selected trial target and present it to the subject as the trial's cue
/// 3) Update the list of trials in "ExpSetup" with the current trial removed
/// 
/// Place this code on the same game object as the ExpSetup.m script
/// </summary>

using UnityEngine;
using System.Collections.Generic;

public class ExpCueConj : MonoBehaviour
{
    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject cubeCue;
    public GameObject starCue;

    [HideInInspector] public GameObject activeTarget;
    private GameObject cueClone;
    private GameObject targetCueClone;

    private ExpSetup expSetupRef;
    private Vector3 fixationLoc;
    private Vector3 shapeCueLoc;

    void Start()
    {
        expSetupRef = this.GetComponent<ExpSetup>();                        // access ExpSetup.m script on the same gameobject as this script
        fixationLoc = GameObject.Find("FixationCross").transform.position;  // access fixation cross gameobject
    }

    public void ShowCue()
    {
        // get list of trials from ExpSetup.m
        List<GameObject> tTrials = expSetupRef.targetTrials;
        Debug.Log("There are " + tTrials.Count + " trials left.");

        // select one of the trials at random and update the list of trials with the selected trial removed
        int n = Random.Range(0, tTrials.Count);
        activeTarget = tTrials[n];
        tTrials.RemoveAt(n);

        // find proper cue direction object for the trial
        bool trialDirection = activeTarget.GetComponent<TargetMotion>().moveRight;

        // display direction cue (left/right)
        if (trialDirection)          // if right motion true
        {
            cueClone = Instantiate(rightArrow, fixationLoc, rightArrow.transform.rotation) as GameObject;
            cueClone.tag = "Clone"; // tag new gameobject as "Clone" for easy deletion later
        }
        else if (!trialDirection)    // if left motion true
        {
            cueClone = Instantiate(leftArrow, fixationLoc, leftArrow.transform.rotation) as GameObject;
            cueClone.tag = "Clone"; // tag new gameobject as "Clone" for easy deletion later
        }
        
        // find proper shape cue object for the trial using the target gameobject's name to search
        // get last four letters of the gameobject's name (Cube/Star)
        string targName = activeTarget.name;
        int a = targName.Length - 4;
        string targShape = targName.Substring(a);

        // get location for shape cue
        shapeCueLoc = new Vector3(fixationLoc.x, fixationLoc.y - 0.5f, fixationLoc.z); 

        // display shape cue (cube/star)
        if (targShape == cubeCue.name)
        {
            targetCueClone = Instantiate(cubeCue, shapeCueLoc, cubeCue.transform.rotation) as GameObject;
            targetCueClone.tag = "Clone"; // tag new gameobject as "Clone" for easy deletion later
        }
        else if (targShape == starCue.name)
        {
            targetCueClone = Instantiate(starCue, shapeCueLoc, starCue.transform.rotation) as GameObject;
            targetCueClone.tag = "Clone"; // tag new gameobject as "Clone" for easy deletion later
        }
    }
}