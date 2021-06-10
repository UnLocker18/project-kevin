﻿using System.Collections.Generic;
using UnityEngine;
using Filo;


public class Rope : Interactable
{
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
    }

    public override int Interact(Transform mainCharacter)
    {
        if (!isInteractable) return -1;

        //Grab(mainCharacter);
        transform.Translate(Vector3.down);
        rigidbody.isKinematic = true;

        return -1;
    }

    public void GenerateRope(Transform mainCharacter)
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

        if (stickObjects.Count == 0)
        {
            mainCharacter.Find("RopeTeleport").GetComponent<RopeTeleport>().Teleport(transform);
            rigidbody.isKinematic = false;
        }
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

        if (obj.GetComponentInChildren<CableBody>().GetType()==typeof(CableDisc)){

        link.type = Cable.Link.LinkType.Rolling;
        
        }

        else {

        link.type = Cable.Link.LinkType.Attachment;
        
        }

        link.body = obj.GetComponentInChildren<CableBody>();

        //link.outAnchor = Vector3.up * 0.5f;

        return link;
    }
}
