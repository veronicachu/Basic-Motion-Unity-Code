﻿using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;

public class TrialInfoData : MonoBehaviour {

    StringBuilder csv = new StringBuilder();
    private string expPath;

    // Script references
    private ExpCue expCueRef;
    public GameObject rightFlickerObject;
    public GameObject leftFlickerObject;

    // Variable references
    private FlickerMaterial rightFlickerRef;
    private FlickerMaterial leftFlickerRef;

    void Start()
    {
        // Initialize variables
        expCueRef = GameObject.Find("ExperimentManager").GetComponent<ExpCue>();
        rightFlickerRef = rightFlickerObject.GetComponent<FlickerMaterial>();
        leftFlickerRef = leftFlickerObject.GetComponent<FlickerMaterial>();

        // Start new stream writer
        expPath = FileName();
        //Debug.Log(expPath);

        // Write first line with data information
        string newLine = string.Format("{0},{1},{2},{3},{4}",
            "Target Direction", "Right Freq", "Left Freq", "Number Targets", "Response");
        csv.AppendLine(newLine);
    }

    public static string FileName()
    {
        Directory.CreateDirectory(Application.dataPath + "/ExpData");

        // Creates the file path to store into the Data folder
        return string.Format("{0}/ExpData/TrialInfo_{1}.csv",
                             Application.dataPath,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void WriteData(int nTargets, int response)
    {
        // Collects target direction
        string targDirection = expCueRef.activeTarget.name;
        targDirection = targDirection.Remove(targDirection.Length - 13);     // remove "Motion_Sphere" at end
        
        // Collect color frequencies in the trial
        string rightFreq = rightFlickerRef.Frequency.ToString();
        string leftFreq = leftFlickerRef.Frequency.ToString();
        
        // Writes a line with target and frequency information
        string newLine = string.Format("{0},{1},{2},{3},{4}",
            targDirection, rightFreq, leftFreq, nTargets, response);
        csv.AppendLine(newLine);
    }

    public void SaveData()
    {
        File.WriteAllText(expPath, csv.ToString());
    }

}
