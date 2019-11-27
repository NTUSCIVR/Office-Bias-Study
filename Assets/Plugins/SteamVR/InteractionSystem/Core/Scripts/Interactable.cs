//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: This object will get hover events and can be attached to the hands
//
//=============================================================================

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    public class Interactable : MonoBehaviour
    {
        public Material highLightMaterial;
        public bool isInteracted;
        public bool canRun;
        private MeshRenderer bodyMeshRenderer;

        private bool IsInteracted
        {
            get
            {
                return isInteracted;
            }

            set
            {
                isInteracted = value;
            }
        }

        [HideInInspector]
        public bool isAttachedToHand;

        public delegate void OnAttachedToHandDelegate(Hand hand);
        public delegate void OnDetachedFromHandDelegate(Hand hand);

        [HideInInspector]
        public event OnAttachedToHandDelegate onAttachedToHand;
        [HideInInspector]
        public event OnDetachedFromHandDelegate onDetachedFromHand;

        //-------------------------------------------------
        void Awake()
        {
            bodyMeshRenderer = transform.Find("highlight").gameObject.GetComponent<MeshRenderer>();
            bodyMeshRenderer.material = highLightMaterial;
            bodyMeshRenderer.enabled = false;
            //isInteracted = false;
            canRun = true;
        }

        //-------------------------------------------------
        private void OnAttachedToHand(Hand hand)
        {
            if (onAttachedToHand != null)
            {
                onAttachedToHand.Invoke(hand);
                isInteracted = true;
                isAttachedToHand = true;
            }
        }


        //-------------------------------------------------
        private void OnDetachedFromHand(Hand hand)
        {
            if (onDetachedFromHand != null)
            {
                onDetachedFromHand.Invoke(hand);
                isAttachedToHand = false;
            }
        }
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand)
        {
            if (canRun)
                ShowHighlight();
        }


        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand)
        {
            HideHighlight();
        }

        //-------------------------------------------------
        public void ShowHighlight()
        {
            //if (renderModelLoaded == false)
            //{
            //    return;
            //}

            if (bodyMeshRenderer != null)
            {
                bodyMeshRenderer.enabled = true;
            }
        }


        //-------------------------------------------------
        public void HideHighlight()
        {
            //if (renderModelLoaded == false)
            //{
            //    return;
            //}

            if (bodyMeshRenderer != null)
            {
                bodyMeshRenderer.enabled = false;
            }
        }
    }
}
