using UnityEngine;
using System.Collections.Generic;

public class RopeLinkable : MonoBehaviour
{
    [SerializeField] private bool isConnected = false;
    [SerializeField] public List<Rope> connectedRopes = new List<Rope>();

    private Outline outline;
    
    public void Connect(int currentPersonality, Rope rope)
    {
        if (!connectedRopes.Contains(rope)) connectedRopes.Add(rope);

        SetUpOutline(rope.ropeColor);

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
        if (connectedRopes.Contains(rope)) connectedRopes.Remove(rope);

        SetUpOutline(rope.ropeColor);

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
            //SetUpOutLine(rope.ropeColor);           

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

    public void SetOutline(Color color, bool value)
    {
        if (isConnected) value = true;

        float H = 0f, S = 0f, V = 0f;
        Color.RGBToHSV(color, out H, out S, out V);
        S = 0.3f;
        color = Color.HSVToRGB(H, S, V);
        color.a = 0.7f;

        SetUpOutline(color);
        outline.enabled = value;
    }

    private void SetUpOutline(Color color)
    {
        outline = GetComponent<Outline>();

        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }
        
        float H = 0f, S = 0f, V = 0f;
        Color.RGBToHSV(color, out H, out S, out V);

        foreach (Rope rope in connectedRopes)
        {
            float H1 = 0f;

            Color.RGBToHSV(rope.ropeColor, out H1, out S, out V);

            if (H != H1)
            {
                H += H1;
                S = 0.9f;
            }
        }            

        if (connectedRopes.Count > 0) color = Color.HSVToRGB(H, S, V);

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = color;
        outline.OutlineWidth = 3f;

        //outline.enabled = false;
    }
}
