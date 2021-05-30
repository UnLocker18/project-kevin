using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool interactable;
    public Outline outline;

    public abstract void Interact(Transform mainCharacter);

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

    public void SetOutline(bool value)
    {
        if (!interactable) return;

        SetUpOutline();
        outline.enabled = value;
    }

    public void Grab(Transform mainCharacter)
    {
        if (!interactable) return;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (transform.parent != mainCharacter)
        {
            rb.isKinematic = true;
            transform.parent = mainCharacter;
            transform.rotation = mainCharacter.localRotation;
            transform.position = mainCharacter.Find("GrabbingPoint").position;
        }
        else
        {
            transform.parent = null;
            rb.isKinematic = false;
        }
    }
}
