using UnityEngine;
using Valve.VR.InteractionSystem;

public class Handtent : MonoBehaviour {

    enum HT_STATE
    {
        UPRIGHT,
        TOPPLED,

        TOTAL
    }

    private HT_STATE state;
    public bool canRun;

    // Use this for initialization
    void Start ()
    {
        state = HT_STATE.TOPPLED;
        transform.localPosition = new Vector3(-102.83f, 6.884839f, 734.5901f);
        transform.eulerAngles = new Vector3(0f, 30.464f, 474.897f);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        //Trigger got pressed
        if (hand.GetStandardInteractionButtonDown() && canRun && GetComponent<Interactable>().canRun)
        {
            state = HT_STATE.UPRIGHT;
            transform.localPosition = new Vector3(-99.36f, 8.94788f, 737.35f);
            transform.eulerAngles = new Vector3(0f, -90f, -1.142f);
            GetComponent<Interactable>().isInteracted = true;
        }
    }

    private bool SwitchState()
    {
        switch (state)
        {
            case HT_STATE.UPRIGHT:
                state = HT_STATE.TOPPLED;
                transform.localPosition = new Vector3(-102.83f, 6.884839f, 734.5901f);
                transform.eulerAngles = new Vector3(0f, 30.464f, 474.897f);
                break;
            case HT_STATE.TOPPLED:
                state = HT_STATE.UPRIGHT;
                transform.localPosition = new Vector3(-99.36f, 8.94788f, 737.35f);
                transform.eulerAngles = new Vector3(0f, -90f, -1.142f);
                break;
        }
        return false;
    }
}
