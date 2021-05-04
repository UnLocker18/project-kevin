using System;
using UnityEngine;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] private float range = 50f;

    private FloorButton previousTarget = null;
    private bool grabbing = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ButtonInteraction();
        AdditionalControls();
    }

    private void AdditionalControls()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BoxInteraction();
        }
    }

    void ButtonInteraction()
    {
        Vector3 rayOrigin = transform.position + 1 * Vector3.up;
        Vector3 rayDirection = -transform.up;

        Debug.DrawRay(rayOrigin, rayDirection * range, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
        {
            FloorButton button = hit.collider.gameObject.GetComponent<FloorButton>();

            if (previousTarget != button && previousTarget != null) previousTarget.Activate(false);

            previousTarget = button;

            if (button != null)
            {
                button.Activate(true);
            }
        }
        else if (previousTarget != null) previousTarget.Activate(false);
    }

    void BoxInteraction()
    {
        Vector3 rayOrigin = transform.position + 0.25f * Vector3.up;
        Vector3 rayDirection = transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * range, Color.blue);

        RaycastHit hit;

        if (grabbing)
        {
            SmallBox box = transform.Find("SmallBox").GetComponent<SmallBox>();

            if (box != null)
            {
                grabbing = !grabbing;
                box.Grab(gameObject, grabbing, transform.Find("GrabbingPoint").transform.position);
            }
        }
        else
        {
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
            {
                SmallBox box = hit.collider.gameObject.GetComponent<SmallBox>();

                if (box != null)
                {
                    grabbing = !grabbing;
                    box.Grab(gameObject, grabbing, transform.Find("GrabbingPoint").transform.position);
                }
            }
        }        
    }
}
