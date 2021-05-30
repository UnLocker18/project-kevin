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
        cable = cableObj.GetComponent<Cable>();
    }

    private void Start()
    {
        isInteractable = true;
    }

    public override void Interact(Transform mainCharacter)
    {
        Grab(mainCharacter);
    }

    public void GenerateRope()
    {
        if (stickObjects.Count > 1)
        {
            DestroyRope();

            cable.links = new Cable.Link[stickObjects.Count];

            int i = 0;
            foreach (RopeLinkable obj in stickObjects)
            {
                cable.links[i] = CreateLink(obj.gameObject);
                i++;
            }

            InstantiateRope();
        }
        else DestroyRope();
    }

    public void ClearRope()
    {
        foreach (RopeLinkable obj in stickObjects)
        {
            obj.Disconnect();
        }
        
        stickObjects.Clear();
        DestroyRope();
    }

    private void InstantiateRope()
    {
        cableInstance = Instantiate(cableObj);
        //gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    private void DestroyRope()
    {
        if (cableInstance != null)
        {
            Destroy(cableInstance);
            //gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
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
