using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public Color outlineColor = Color.yellow;

    public bool isInteractable;
    public Outline outline;

    private float originalSpeed = 0f;

    public abstract int Interact(Transform mainCharacter);

    public void SetUpOutline()
    {
        outline = gameObject.GetComponent<Outline>();

        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }        

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = 3f;

        outline.enabled = false;
    }

    public virtual void SetOutline(bool value)
    {
        if (!isInteractable) return;

        SetUpOutline();
        outline.enabled = value;
    }

    public void Grab(Transform mainCharacter)
    {
        if (!isInteractable) return;

        Rigidbody rb = GetComponent<Rigidbody>();
        Transform spineTransform = GameObject.FindGameObjectWithTag("GrabParent").transform;

        if (transform.parent != spineTransform)
        {
            rb.isKinematic = true;
            
            transform.rotation = mainCharacter.localRotation;
            transform.position = mainCharacter.Find("GrabbingPoint").position;
            transform.parent = spineTransform;

            GetComponentInChildren<BoxCollider>().enabled = false;
            mainCharacter.GetComponent<BoxCollider>().enabled = true;

            mainCharacter.GetComponent<SimpleThirdPRigidbodyController>().isGrabbing = true;
            originalSpeed = mainCharacter.GetComponent<SimpleThirdPRigidbodyController>()._speed;
            mainCharacter.GetComponent<SimpleThirdPRigidbodyController>()._speed = 1.3f;
            FindObjectOfType<AudioManager>().Play("pickSound");
        }
        else
        {
            mainCharacter.GetComponent<BoxCollider>().enabled = false;
            GetComponentInChildren<BoxCollider>().enabled = true;
            transform.parent = null;
            rb.isKinematic = false;

            mainCharacter.GetComponent<SimpleThirdPRigidbodyController>().isGrabbing = false;
            mainCharacter.GetComponent<SimpleThirdPRigidbodyController>()._speed = originalSpeed;
            FindObjectOfType<AudioManager>().Play("dropSound");
        }
    }
}
