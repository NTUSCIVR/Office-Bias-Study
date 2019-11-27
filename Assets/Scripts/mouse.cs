using System.Diagnostics;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class mouse : MonoBehaviour
{

    public GameObject monitor;
    public Material monitorOff;
    public Material monitorOff1;
    public Material monitorOff2;
    public Material monitorOff3;
    public Material monitorOff4;
    public Material monitorOn;

    enum MON_STATE
    {
        ON,
        OFF,

        TOTAL
    }

    enum MON_OFF_STATE
    {
        O_OFF,
        O_1,
        O_2,
        O_3,
        O_4,

        TOTAL
    }

    private MON_STATE state;
    private MON_OFF_STATE off_state;
    private MeshRenderer monitorRenderer;
    private Stopwatch stopwatch = new Stopwatch(); 
    public bool canRun;

    //Use this for initialization
   void Start ()
    {

       monitorRenderer = monitor.GetComponent<MeshRenderer>();
       state = MON_STATE.OFF;
       off_state = MON_OFF_STATE.O_OFF;
       stopwatch.Start();
       monitorRenderer.material = monitorOff;
    }

    //Update is called once per frame
    void Update()
    {
        if(state == MON_STATE.OFF && stopwatch.ElapsedMilliseconds % 1000 < 10)
        {
            switch(off_state)
            {
                case MON_OFF_STATE.O_OFF:
                    if (canRun)
                    {
                        off_state = MON_OFF_STATE.O_1;
                        monitorRenderer.material = monitorOff1;
                    }
                    break;
                case MON_OFF_STATE.O_1:
                    off_state = MON_OFF_STATE.O_2;
                    monitorRenderer.material = monitorOff2;
                    break;
                case MON_OFF_STATE.O_2:
                    off_state = MON_OFF_STATE.O_3;
                    monitorRenderer.material = monitorOff3;
                    break;
                case MON_OFF_STATE.O_3:
                    off_state = MON_OFF_STATE.O_4;
                    monitorRenderer.material = monitorOff4;
                    break;
                case MON_OFF_STATE.O_4:
                    off_state = MON_OFF_STATE.O_1;
                    monitorRenderer.material = monitorOff1;
                    break;
            }
        }
    }

    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        //Trigger got pressed
        if (hand.GetStandardInteractionButtonDown() && canRun && GetComponent<Interactable>().canRun)
        {
            state = MON_STATE.ON;
            monitorRenderer.material = monitorOn;
            GetComponent<Interactable>().isInteracted = true;
        }
    }

    private bool SwitchState()
    {
        switch (state)
        {
            case MON_STATE.OFF:
                state = MON_STATE.ON;
                monitorRenderer.material = monitorOn;
                break;
            case MON_STATE.ON:
                state = MON_STATE.OFF;
                monitorRenderer.material = monitorOff;
                break;
        }
        return false;
    }
}
