/*
This code will: 
1) Randomly select one of the trials from the list generated by the script "ExpSetup"
2) Take the selected trial target and present it to the subject as a cue
3) Update the list of trials in "ExpSetup" with the current trial removed

Directions: 
1) Place this code on the same game object as the "ExpSetup" script

Debugging:
To see the current trial's target, look at "activeTarget"
*/

using UnityEngine;
using System.Collections.Generic;

public class ExpCue : MonoBehaviour
{
    public GameObject rightArrow;
    public GameObject leftArrow;

    //[HideInInspector]
    public GameObject activeTarget;
    private GameObject cueClone;

    private ExpSetup expSetupRef;
    private Vector3 fixationLoc;

    void Start()
    {
        expSetupRef = this.GetComponent<ExpSetup>();
        fixationLoc = GameObject.Find("FixationCross").transform.position;
    }
    
	public void ShowCue ()
    {
        // get list of trials
        List<GameObject> tTrials = expSetupRef.targetTrials;
        Debug.Log("There are " + tTrials.Count + " trials left.");

        // select one of the trials at random and update the list of trials with the selected trial removed
        int n = Random.Range(0, tTrials.Count);
        activeTarget = tTrials[n];
        tTrials.RemoveAt(n);

        // find proper cue object for the trial
        bool trialDirection = activeTarget.GetComponent<TargetMotion>().moveRight;
        
        // display cue in front of subject
        if (trialDirection)          // right motion true
        {
            cueClone = Instantiate(rightArrow, fixationLoc, rightArrow.transform.rotation) as GameObject;
            cueClone.tag = "Clone";
        }
        else if(!trialDirection)    // left motion true
        {
            cueClone = Instantiate(leftArrow, fixationLoc, leftArrow.transform.rotation) as GameObject;
            cueClone.tag = "Clone";
        }
    }
}
