using UnityEngine;

public class EnvironmentInteractions : MonoBehaviour
{
    [SerializeField] private float range = 50f;

    private FloorButton previousTarget;

    // Start is called before the first frame update
    void Start()
    {
        previousTarget = null;
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
}
