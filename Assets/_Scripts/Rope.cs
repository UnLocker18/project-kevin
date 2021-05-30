using System.Collections.Generic;
using UnityEngine;
using Filo;

public class Rope : Interactable
{
    public List<RopeLinkable> stickObjects = new List<RopeLinkable>();

    Cable cable;
    [SerializeField] private GameObject cableObj;
    private GameObject cableInstance;

    // Use this for initialization
    void Awake()
    {
        interactable = true;
        cable = cableObj.GetComponent<Cable>();
    }

    private void Start()
    {
        SetUpOutline();
    }

    public override void Interact(Transform mainCharacter)
    {
        Grab(mainCharacter);
    }

    public void GenerateRope()
    {
        if (stickObjects.Count > 1)
        {
            if (cableInstance != null) Destroy(cableInstance);

            cable.links = new Cable.Link[stickObjects.Count];

            int i = 0;
            foreach (RopeLinkable obj in stickObjects)
            {
                cable.links[i] = CreateLink(obj.gameObject);
                i++;
            }

            cableInstance = Instantiate(cableObj);
        }
        else if (cableInstance != null) Destroy(cableInstance);
    }

    public void ClearRope()
    {
        foreach (RopeLinkable obj in stickObjects)
        {
            obj.Disconnect();
        }
        
        stickObjects.Clear();
        Destroy(cableInstance);        
    }

    private Cable.Link CreateLink(GameObject obj)
    {
        Cable.Link link = new Cable.Link();
        link.type = Cable.Link.LinkType.Attachment;
        link.body = obj.GetComponentInChildren<CableBody>();
        //link.outAnchor = Vector3.up * 0.5f;

        return link;
    }
}
