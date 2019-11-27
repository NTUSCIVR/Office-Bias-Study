using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{

    public int nextSceneID;
    private Stopwatch stopwatch = new Stopwatch();

    // Use this for initialization
    void Start()
    {
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSceneID == 1)
        {
            if (stopwatch.ElapsedMilliseconds > 12000)
            {
                stopwatch.Stop();
                stopwatch.Reset();
                NextScene();
            }
        }
        else
        {
            if (stopwatch.ElapsedMilliseconds > 10000)
            {
                stopwatch.Stop();
                stopwatch.Reset();
                NextScene();
            }
        }

    }

    void NextScene()
    {
        switch (nextSceneID)
        {
            case 1:
            case 2:
            case 3:
                StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Scene" + nextSceneID));
                break;
            default:
                StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndQuit(SceneFader.FadeDirection.In));
                break;
        }
    }

}
