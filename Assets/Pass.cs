using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Pass : MonoBehaviour
{

    public GameObject realPass;

    // Use this for initialization
    void Start()
    {
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
            meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
            meshRenderer.enabled = realPass.GetComponent<Interactable>().isAttachedToHand;
    }
}
