using UnityEngine;

public class SmallBox : MonoBehaviour
{
    [SerializeField] private float range = 0.2f;

    private FloorButton previousTarget = null;

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
        FloorButton button = collision.gameObject.GetComponent<FloorButton>();

        if (button != null)
        {
            button.Activate(true, true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        FloorButton button = collision.gameObject.GetComponent<FloorButton>();

        if (button != null)
        {
            button.Activate(false, true);
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
