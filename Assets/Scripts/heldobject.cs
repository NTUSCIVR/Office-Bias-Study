using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class heldobject : MonoBehaviour {

    [HideInInspector]
    public controller parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="Door")
        {
            Destroy(collision.gameObject);
        }
    }
}
