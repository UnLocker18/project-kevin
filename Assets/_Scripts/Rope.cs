using System.Collections.Generic;
using UnityEngine;
using Filo;

public class Rope : MonoBehaviour
{
    //public Piston stickParent;

    //trasformare lista di smallbox in lista di transform?
    //public List<SmallBox> stickChildren = new List<SmallBox>();
        
    //private float tenseDistance;

    public List<GameObject> stickObjects = new List<GameObject>();

    Cable cable;
    public GameObject cableObj;
    private GameObject cableInstance;

    // Use this for initialization
    void Awake()
    {
        cable = cableObj.GetComponent<Cable>();
    }

    public void GenerateRope(GameObject player)
    {
        if (stickObjects.Count == 1)
        {
            cable.links = new Cable.Link[2];

            Cable.Link parentLink = new Cable.Link();
            parentLink.type = Cable.Link.LinkType.Attachment;
            parentLink.body = stickObjects[0].GetComponentInChildren<CableBody>();
            //parentLink.outAnchor = Vector3.up * 0.5f;
            cable.links[0] = parentLink;

            if (stickObjects[0].GetComponent<Piston>() != null)
                stickObjects[0].GetComponent<Piston>().isConnected = true;

            if (stickObjects[0].GetComponent<SmallBox>() != null)
                stickObjects[0].GetComponent<SmallBox>().isConnected = true;

            Cable.Link childlink = new Cable.Link();
            childlink.type = Cable.Link.LinkType.Attachment;
            childlink.body = player.GetComponent<CableBody>();
            //parentLink.outAnchor = Vector3.up * 0.5f;
            cable.links[1] = childlink;

            cableInstance = Instantiate(cableObj);
        }

        //if (cable != null && stickParent != null && stickChildren.Count > 0)
        //{
        //    if (cableInstance == null)
        //    {
        //        cable.links = new Cable.Link[1 + stickChildren.Count];

        //        Cable.Link parentLink = new Cable.Link();
        //        parentLink.type = Cable.Link.LinkType.Attachment;
        //        parentLink.body = stickParent.gameObject.GetComponentInChildren<CableBody>();
        //        //parentLink.outAnchor = Vector3.up * 0.5f;
        //        cable.links[0] = parentLink;

        //        stickParent.isConnected = true;

        //        int i = 1;
        //        foreach (SmallBox child in stickChildren)
        //        {
        //            if (!child.isConnected)
        //            {
        //                Cable.Link childLink = new Cable.Link();
        //                childLink.type = Cable.Link.LinkType.Attachment;
        //                childLink.body = child.gameObject.GetComponentInChildren<CableBody>();
        //                //childLink.outAnchor = Vector3.up * 0.5f;
        //                cable.links[i] = childLink;

        //                child.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        //                child.isConnected = true;
        //            }

        //            i++;
        //        }

        //        cableInstance = Instantiate(cableObj);
        //    }
        //    else
        //    {
        //        //update cableInstance?
        //    }
        //}
    }

    public void RemoveRope(SmallBox currentSb)
    {
        //stickChildren[0].gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        //stickParent.isConnected = false;
        //stickChildren[0].isConnected = false;

        //stickParent = null;
        //stickChildren.Clear();

        //Destroy(cableInstance);        
    }

    public void Grab(GameObject mainCharacter, bool grabbing, Vector3 grabbingPoint)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (grabbing)
        {
            rb.isKinematic = true;
            transform.parent = mainCharacter.transform;
            transform.rotation = mainCharacter.transform.localRotation;
            transform.position = grabbingPoint;
        }
        else
        {
            transform.parent = null;
            rb.isKinematic = false;
        }
    }

    //public void MoveRope()
    //{
    //    if (stickParent != null && stickChildren.Count > 0)
    //    {
    //        if (Vector3.Magnitude(stickParent.transform.position - stickChildren[0].transform.position) < tenseDistance)
    //        {
    //            foreach (SmallBox child in stickChildren)
    //            {
    //                child.transform.SetParent(null);
    //            }
    //        }
    //        else
    //        {
    //            foreach (SmallBox child in stickChildren)
    //            {
    //                child.transform.SetParent(stickParent.transform.GetChild(0));
    //            }
    //        }
    //    }
    //}

    //public void ApplyRope()
    //{
    //    if (stickParent != null)
    //    {
    //        foreach (SmallBox child in stickParent.GetComponentsInChildren<SmallBox>())
    //        {
    //            if (!stickChildren.Contains(child)) child.transform.SetParent(null);
    //        }
    //        foreach (SmallBox child in stickChildren)
    //        {
    //            child.transform.SetParent(stickParent.transform.GetChild(0));
    //        }

    //        if (stickChildren.Count > 0)
    //        {
    //            if (stickParent.isExtended)
    //                tenseDistance = Vector3.Magnitude(stickParent.transform.position - stickChildren[0].transform.position);
    //            else
    //                tenseDistance = Vector3.Magnitude(stickParent.transform.position - stickChildren[0].transform.position) + stickParent.range;
    //        }
    //    }
    //    else
    //    {
    //        foreach (SmallBox child in stickChildren)
    //        {
    //            child.transform.SetParent(null);
    //        }
    //    }
    //}3    
}
