using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(controller))]
public class hand : MonoBehaviour
{
    GameObject heldobject;
    controller Controller;

    Rigidbody simulator;
    // Use this for initialization
    void Start()
    {
        simulator = new GameObject().AddComponent<Rigidbody>();
        simulator.name = "simulator";
        simulator.transform.parent = transform.parent;

        Controller = GetComponent<controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heldobject)
        {
            simulator.velocity = (transform.position - simulator.position) * 50.0f;
            if (Controller.Controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                heldobject.transform.parent = null;
                heldobject.GetComponent<Rigidbody>().isKinematic = false;
                heldobject.GetComponent<Rigidbody>().velocity = simulator.velocity;
                heldobject.GetComponent<heldobject>().parent = null;
                heldobject = null;
            }
        }
        else
        {
            if (Controller.Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);
                foreach(Collider col in cols)
                {
                    if(heldobject==null&&col.GetComponent<heldobject>()&&col.GetComponent<heldobject>().parent==null)
                    {
                        heldobject = col.gameObject;
                        heldobject.transform.parent = transform;
                        heldobject.transform.localPosition = Vector3.zero;
                        heldobject.transform.localRotation = Quaternion.identity;
                        heldobject.GetComponent<Rigidbody>().isKinematic = true;
                        heldobject.GetComponent<heldobject>().parent = Controller;
                        if(heldobject.gameObject.name=="Notebook (4)")
                        {
                            heldobject = col.gameObject;
                            heldobject.transform.parent = transform;
                            heldobject.transform.localPosition =new Vector3(1f,-0.5f,0.5f);
                            heldobject.transform.localRotation = Quaternion.identity;
                            heldobject.GetComponent<Rigidbody>().isKinematic = true;
                            heldobject.GetComponent<heldobject>().parent = Controller;
                        }
                    }
                }

            }
        }
    }
}
