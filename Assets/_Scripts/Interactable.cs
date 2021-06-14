using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool isInteractable;
    public Outline outline;

    public abstract int Interact(Transform mainCharacter);

    public void SetUpOutline()
    {
        outline = gameObject.GetComponent<Outline>();

        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }        

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.yellow;
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

        if (transform.parent != mainCharacter)
        {
            rb.isKinematic = true;
            transform.parent = mainCharacter;
            transform.rotation = mainCharacter.localRotation;
            transform.position = mainCharacter.Find("GrabbingPoint").position;
            FindObjectOfType<AudioManager>().Play("pickSound");
            mainCharacter.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            mainCharacter.GetComponent<BoxCollider>().enabled = false;
            transform.parent = null;
            rb.isKinematic = false;
            FindObjectOfType<AudioManager>().Play("dropSound");
        }
    }
}
