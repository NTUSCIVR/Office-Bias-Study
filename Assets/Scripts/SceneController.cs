using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;

    public GameObject pass;
    public GameObject nametent;
    public GameObject mouse;
    public GameObject penRed;
    public GameObject penGreen;
    public GameObject penYellow;
    public GameObject penBlue;

    private Stopwatch stopwatch = new Stopwatch();

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        nametent.GetComponent<Interactable>().canRun = false;
        mouse.GetComponent<Interactable>().canRun = false;
        penRed.GetComponent<Interactable>().canRun = false;
        penGreen.GetComponent<Interactable>().canRun = false;
        penYellow.GetComponent<Interactable>().canRun = false;
        penBlue.GetComponent<Interactable>().canRun = false;

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        nametent.GetComponent<Interactable>().canRun = false;
        mouse.GetComponent<Interactable>().canRun = false;
        penRed.GetComponent<Interactable>().canRun = false;
        penGreen.GetComponent<Interactable>().canRun = false;
        penYellow.GetComponent<Interactable>().canRun = false;
        penBlue.GetComponent<Interactable>().canRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pass.GetComponent<Interactable>().isInteracted
            && !nametent.GetComponent<Handtent>().canRun)
        {
            nametent.GetComponent<Interactable>().canRun = true;
            nametent.GetComponent<Handtent>().canRun = pass.GetComponent<Interactable>().isInteracted;
            pass.GetComponent<Interactable>().canRun = false;
        }
        if (nametent.GetComponent<Interactable>().isInteracted
            && !mouse.GetComponent<mouse>().canRun)
        {
            mouse.GetComponent<Interactable>().canRun = true;
            mouse.GetComponent<mouse>().canRun = nametent.GetComponent<Interactable>().isInteracted;
            nametent.GetComponent<Interactable>().canRun = false;
        }
        if (mouse.GetComponent<Interactable>().isInteracted
            && !penRed.GetComponent<pen>().canRun
            && !penGreen.GetComponent<pen>().canRun
            && !penYellow.GetComponent<pen>().canRun
            && !penBlue.GetComponent<pen>().canRun)
        {
            penRed.GetComponent<Interactable>().canRun = true;
            penGreen.GetComponent<Interactable>().canRun = true;
            penYellow.GetComponent<Interactable>().canRun = true;
            penBlue.GetComponent<Interactable>().canRun = true;
            penRed.GetComponent<pen>().canRun = mouse.GetComponent<Interactable>().isInteracted;
            penGreen.GetComponent<pen>().canRun = mouse.GetComponent<Interactable>().isInteracted;
            penYellow.GetComponent<pen>().canRun = mouse.GetComponent<Interactable>().isInteracted;
            penBlue.GetComponent<pen>().canRun = mouse.GetComponent<Interactable>().isInteracted;
            mouse.GetComponent<Interactable>().canRun = false;
        }
        if (pass.GetComponent<Interactable>().isInteracted
            && nametent.GetComponent<Interactable>().isInteracted
            && mouse.GetComponent<Interactable>().isInteracted
            && penRed.GetComponent<Interactable>().isInteracted && penGreen.GetComponent<Interactable>().isInteracted && penYellow.GetComponent<Interactable>().isInteracted && penBlue.GetComponent<Interactable>().isInteracted)
        {
            stopwatch.Start();
            //SceneManager.LoadSceneAsync("Meetingroom");
        }
        if (stopwatch.IsRunning && stopwatch.ElapsedMilliseconds >= 3000)
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "Scene1toScene2"));
    }
}
