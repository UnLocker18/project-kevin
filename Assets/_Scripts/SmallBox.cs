﻿using UnityEngine;

public class SmallBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grab(GameObject mainCharacter, bool grabbing, Vector3 grabbingPoint)
    {
        if (grabbing)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            //transform.Translate(Vector3.up);
            transform.parent = mainCharacter.transform;
            transform.rotation = mainCharacter.transform.localRotation;
            transform.position = grabbingPoint;
        }
        else
        {
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
