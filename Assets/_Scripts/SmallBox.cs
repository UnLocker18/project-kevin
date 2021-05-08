using UnityEngine;

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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Trigger")
        {
            FloorButton button = collision.gameObject.GetComponentInParent<FloorButton>();

            if (button != null)
            {
                //button.Activate(true, true);
                button.Activate(true);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Trigger")
        {
            FloorButton button = collision.gameObject.GetComponentInParent<FloorButton>();

            if (button != null)
            {
                //button.Activate(false, true);
                button.Deactivate(true);
            }
        }
    }

    public void Grab(GameObject mainCharacter, bool grabbing, Vector3 grabbingPoint)
    {
        if (grabbing)
        {
            GetComponent<Rigidbody>().isKinematic = true;
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
