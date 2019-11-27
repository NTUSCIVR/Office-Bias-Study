using UnityEngine;
using Valve.VR.InteractionSystem;

public class pen : MonoBehaviour
{

    public GameObject holder;
    public PEN_TYPE type;

    //enum PEN_STATE
    //{
    //    OUT,
    //    IN,

    //    TOTAL
    //}
    public enum PEN_TYPE
    {
        RED,
        GREEN,
        BLUE,
        YELLOW,

        TOTAL
    }

    //private PEN_STATE state;
    private Vector3 holderpos;
    public bool canRun;

    // Use this for initialization
    void Start()
    {
        //state = PEN_STATE.OUT;
        holderpos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (holderpos != holder.transform.localPosition)
            holderpos = holder.transform.localPosition;
    }

    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        //Trigger got pressed
        if (hand.GetStandardInteractionButtonDown() && canRun && GetComponent<Interactable>().canRun)
        {
            //SwitchState();
            transform.localPosition = new Vector3(holderpos.x - 260f, holderpos.y + 810f, holderpos.z + 950f);
            transform.localEulerAngles = new Vector3(76, -224, -271);
            GetComponent<Interactable>().isInteracted = true;
        }
    }

    //private bool SwitchState()
    //{
    //    switch (state)
    //    {
    //        case PEN_STATE.OUT:
    //            state = PEN_STATE.IN;
    //            break;
    //        case PEN_STATE.IN:
    //            state = PEN_STATE.OUT;
    //            switch (type)
    //            {
    //                case PEN_TYPE.RED:
    //                    transform.localPosition = new Vector3(272f, 60f, 105f);
    //                    transform.localEulerAngles = new Vector3(11, -26, -51);
    //                    break;
    //                default:
    //                    break;
    //            }
    //            break;
    //    }
    //    return false;
    //}
}
