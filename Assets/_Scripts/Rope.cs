using System.Collections.Generic;
using UnityEngine;
using Filo;


public class Rope : Interactable
{
    [SerializeField] public Color ropeColor = Color.green;
    public List<RopeLinkable> stickObjects = new List<RopeLinkable>();

    private Vector3 startPostition;
    private Rigidbody rigidbody;

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
        startPostition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        outlineColor = ropeColor;
    }

    public override int Interact(Transform mainCharacter)
    {
        if (!isInteractable) return -1;

        //Grab(mainCharacter);

        //transform.Translate(Vector3.down);

        transform.position = transform.position - new Vector3(0f, 3f, 0f);
        //transform.eulerAngles = new Vector3(0f, 0f, 0f);
        rigidbody.isKinematic = true;

        return -1;
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

        DropRope();
    }

    public void DropRope()
    {
        if (stickObjects.Count == 0)
        {
            FindObjectOfType<RopeTeleport>().Teleport(transform);
            rigidbody.isKinematic = false;
        }
    }

    private void InstantiateRope()
    {
        cableInstance = Instantiate(cableObj);
    }

    private void DestroyRope()
    {
        if (cableInstance != null)
        {
            Destroy(cableInstance);
        }
    }

    private Cable.Link CreateLink(GameObject obj)
    {
        Cable.Link link = new Cable.Link();

        if (obj.GetComponentInChildren<CableBody>().GetType()==typeof(CableDisc))
        {
            link.type = Cable.Link.LinkType.Rolling;        
        }

        else
        {
            link.type = Cable.Link.LinkType.Attachment;        
        }

        link.body = obj.GetComponentInChildren<CableBody>();

        //link.outAnchor = Vector3.up * 0.5f;

        return link;
    }

    //public void ClearRope()
    //{
    //    foreach (RopeLinkable obj in stickObjects)
    //    {
    //        obj.Disconnect();
    //    }

    //    stickObjects.Clear();
    //    DestroyRope();
    //}
}
