using System.IO;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class sphere : MonoBehaviour
{
    public bool canRun;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        //Trigger got pressed
        if (hand.GetStandardInteractionButtonDown() && canRun && GetComponent<Interactable>().canRun)
        {
            
        }
    }
}
