/* 
This code will:
1) Pre-generate a list of all of the possible target trials when the scene loads
2) Pre-generate a list of all of the possible flicker combinations when the scene loads

Directions: 
1) Place this code on a controller game object
2) Enter the number of total trials into "totalTrials" (make sure multiple of # of cuetypes)
3) Enter the number of possible targets into "cueTypes" size
4) Drag the possible target objects into the list's elements

Debugging:
To see the list of trials, look at "targetTrials"
*/

using UnityEngine;
using System.Collections.Generic;

public class ExpSetup : MonoBehaviour
{
    public int totalTrials;
    public List<GameObject> cueTypes = new List<GameObject>();
    public List<float> frequencies = new List<float>();

    [HideInInspector] public int trialsPerCue;
    [HideInInspector] public bool loadComplete = false;
    
    [HideInInspector]
    public List<GameObject> targetTrials = new List<GameObject>();
    [HideInInspector]
    public List<float> rightfreqTrials = new List<float>();
    [HideInInspector]
    public List<float> leftfreqTrials = new List<float>();

    [HideInInspector] public List<bool> right12peripheralTrials = new List<bool>();
    [HideInInspector] public List<bool> right18peripheralTrials = new List<bool>();
    [HideInInspector] public List<bool> left12peripheralTrials = new List<bool>();
    [HideInInspector] public List<bool> left18peripheralTrials = new List<bool>();

    public void Awake ()
    {
        // this loop creates the motion targets for trials
        trialsPerCue = totalTrials / cueTypes.Count;
        int numTrials = cueTypes.Count;
        for (int j = 0; j < trialsPerCue; j++)
        {
            for (int i = 0; i < numTrials; i++)
            {
                targetTrials.Add(cueTypes[i]);
            }
        }

        // this loop creates the motion-freq for trials
        for (int i = 0; i < totalTrials / 4; i++)
        {
            rightfreqTrials.AddRange(frequencies);
            leftfreqTrials.AddRange(frequencies);
        }

        // this loop creates the peripheral direction for trials
        for (int i = 0; i < totalTrials / 8; i++)
        {
            right12peripheralTrials.Add(true);
            right12peripheralTrials.Add(false);

            right18peripheralTrials.Add(true);
            right18peripheralTrials.Add(false);

            left12peripheralTrials.Add(true);
            left12peripheralTrials.Add(false);

            left18peripheralTrials.Add(true);
            left18peripheralTrials.Add(false);
        }

        loadComplete = true;
    }
}