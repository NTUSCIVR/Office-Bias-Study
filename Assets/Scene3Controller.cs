using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Scene3Controller : MonoBehaviour
{

    public GameObject leftSphere;
    public GameObject rightSphere;
    public GameObject thirdOption;
    public GameObject textBox;
    public SteamVR_LaserPointer leftHand;
    public SteamVR_LaserPointer rightHand;
    private Stopwatch stopwatch = new Stopwatch();
    private bool choiceDisplayed;

    // Use this for initialization
    void Start()
    {
        leftSphere.SetActive(false);
        rightSphere.SetActive(false);
        thirdOption.SetActive(false);
        textBox.SetActive(false);
        leftHand.enabled = false;
        rightHand.enabled = false;
        stopwatch.Reset();
        choiceDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (choiceDisplayed)
        {
            
        }
        else if (!GetComponent<AudioSource>().isPlaying)
        {
            leftSphere.SetActive(true);
            rightSphere.SetActive(true);
            thirdOption.SetActive(true);
            textBox.SetActive(true);
            leftHand.enabled = true;
            rightHand.enabled = true;
            stopwatch.Reset();
            stopwatch.Start();
            choiceDisplayed = true;
        }
    }

    public void /*Avengers: */EndGame(string choice = "") // true is confront false is exit
    {
        stopwatch.Stop();
        PlayerPrefs.SetFloat("TimeToDecide", stopwatch.ElapsedMilliseconds);
        leftSphere.SetActive(false);
        rightSphere.SetActive(false);
        thirdOption.SetActive(false);
        leftHand.enabled = false;
        rightHand.enabled = false;
        stopwatch.Reset();
        choiceDisplayed = false;
        string sChoice = choice;
        PlayerPrefs.SetString("Choice", sChoice);
        string filePath = Application.dataPath + "/Saved_data.csv";
        File.AppendAllText(filePath, System.Environment.NewLine + PlayerPrefs.GetString("ID") + "," + (PlayerPrefs.GetFloat("TimeToDecide") / 1000) + "s" + "," + PlayerPrefs.GetString("Choice"));
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Scene3toEnd"));
    }
}
