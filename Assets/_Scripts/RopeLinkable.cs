using UnityEngine;

public class RopeLinkable : MonoBehaviour
{
    [SerializeField] private bool isConnected = false;

    private Outline outline;
    
    public void Connect()
    {
        SetUpOutLine();

        isConnected = true;
        outline.enabled = true;

        HandleRotation();
    }

    public void Disconnect()
    {
        SetUpOutLine();

        isConnected = false;
        outline.enabled = false;

        HandleRotation();
    }

    private void HandleRotation()
    {
        SmallBox sb = GetComponent<SmallBox>();
        if (sb != null)
        {
            sb.GetComponent<Rigidbody>().freezeRotation = isConnected;
            sb.isInteractable = !isConnected;
        }
    }

    private void SetUpOutLine()
    {
        outline = GetComponent<Outline>();

        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 3f;

        outline.enabled = false;
    }
}
