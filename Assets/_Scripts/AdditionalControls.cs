using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalControls : MonoBehaviour
{
    private EnvironmentInteractions environmentInteractions;
    // Start is called before the first frame update
    void Start()
    {
        environmentInteractions = gameObject.GetComponent<EnvironmentInteractions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            environmentInteractions.Interaction();
        }
    }
}
