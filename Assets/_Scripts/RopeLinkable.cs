using UnityEngine;
using System.Collections.Generic;

public class RopeLinkable : MonoBehaviour
{
    [SerializeField] private bool isConnected = false;
    [SerializeField] public List<Rope> connectedRopes = new List<Rope>();

    private Outline outline;
    
    public void Connect(int currentPersonality, Rope rope)
    {
        SetUpOutLine();

        if (!connectedRopes.Contains(rope)) connectedRopes.Add(rope);

        if (connectedRopes.Count > 0)
        {
            isConnected = true;
            outline.enabled = true;
        }

        rope.stickObjects.Add(this);
        rope.GenerateRope();

        HandleRotation(currentPersonality);
    }

    public void Disconnect(int currentPersonality, Rope rope)
    {
        SetUpOutLine();

        if (connectedRopes.Contains(rope)) connectedRopes.Remove(rope);

        if (connectedRopes.Count == 0)
        {
            isConnected = false;
            outline.enabled = false;
        }

        rope.stickObjects.Remove(this);
        rope.GenerateRope();

        HandleRotation(currentPersonality);
    }

    public void DisconnectAll(int currentPersonality)
    {
        foreach (Rope rope in connectedRopes)
        {
            SetUpOutLine();           

            rope.stickObjects.Remove(this);
            rope.GenerateRope();

            HandleRotation(currentPersonality);
        }

        connectedRopes.Clear();

        if (connectedRopes.Count == 0)
        {
            isConnected = false;
            outline.enabled = false;
        }
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

        //outline.enabled = false;
    }
}
