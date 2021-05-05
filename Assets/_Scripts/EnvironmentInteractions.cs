using UnityEngine;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] private float range = 1.3f;

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
    }

    void ButtonInteraction()
    {
        Vector3 rayOrigin = transform.position + 1 * Vector3.up;
        Vector3 rayDirection = -transform.up;

        Debug.DrawRay(rayOrigin, rayDirection * range, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, range))
        {
            FloorButton button = hit.collider.gameObject.GetComponentInParent<FloorButton>();

            if (previousTarget != button && previousTarget != null) previousTarget.Activate(false, false);

            previousTarget = button;

            if (button != null)
            {
                button.Activate(true, false);
            }
        }
        else if (previousTarget != null) previousTarget.Activate(false, false);
    }

    public void BoxInteraction()
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
