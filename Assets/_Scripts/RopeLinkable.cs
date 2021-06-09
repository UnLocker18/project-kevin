using UnityEngine;

public class RopeLinkable : MonoBehaviour
{
    [SerializeField] private bool isConnected = false;

    private Outline outline;
    
    public void Connect(int currentPersonality)
    {
        SetUpOutLine();

        isConnected = true;
        outline.enabled = true;

        HandleRotation(currentPersonality);
    }

    public void Disconnect(int currentPersonality)
    {
        SetUpOutLine();

        isConnected = false;
        outline.enabled = false;

        HandleRotation(currentPersonality);
    }

    private void HandleRotation(int currentPersonality)
    {
        SmallBox sb = GetComponent<SmallBox>();
        if (sb != null)
        {
            sb.GetComponent<Rigidbody>().freezeRotation = isConnected;
            sb.isLocked = isConnected;
            sb.ToggleInteractability(currentPersonality);
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
