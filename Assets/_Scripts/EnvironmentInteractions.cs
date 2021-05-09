using UnityEngine;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] private float range = 1.3f;
    
    private bool grabbing = false;

    public void Interaction()
    {
        Vector3 rayOrigin = transform.position + 0.25f * Vector3.up;
        Vector3 rayDirection = transform.forward;

        Debug.DrawRay(rayOrigin, rayDirection * range, Color.blue);

        RaycastHit hit;

        if (grabbing)
        {
            SmallBox smallBox = transform.GetComponentInChildren<SmallBox>();

            if(smallBox != null)
            {
                grabbing = !grabbing;
                smallBox.Grab(gameObject, grabbing, transform.Find("GrabbingPoint").transform.position);
            }
        }
        else
        {
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
            {
                BoxInteraction(hit);
                PuzzleButtonInteraction(hit);
            }
        }        
    }

    void BoxInteraction(RaycastHit hit)
    {
        SmallBox box = hit.collider.gameObject.GetComponent<SmallBox>();

        if (box != null)
        {
            grabbing = !grabbing;
            box.Grab(gameObject, grabbing, transform.Find("GrabbingPoint").transform.position);
        }
    }

    void PuzzleButtonInteraction(RaycastHit hit)
    {
        PuzzleButton puzzleButton = hit.collider.gameObject.GetComponentInParent<PuzzleButton>();

        if (puzzleButton != null)
        {
            puzzleButton.Toggle();
        }
    }
}
