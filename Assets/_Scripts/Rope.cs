using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Filo;

public class Rope : MonoBehaviour
{
    public Piston stickParent;

    //trasformare lista di smallbox in lista di transform?
    public List<SmallBox> stickChildren = new List<SmallBox>();
    private float tenseDistance;

    Cable cable;
    public GameObject cableObj;

    // Use this for initialization
    void Awake()
    {
        cable = cableObj.GetComponent<Cable>();
    }

    public void GenerateMesh()
    {
        Debug.Log("generate mesh");
        if (cable != null && stickParent != null && stickChildren.Count > 0)
        {
            cable.links = new Cable.Link[2];

            Cable.Link parentLink = new Cable.Link();
            parentLink.type = Cable.Link.LinkType.Attachment;
            parentLink.body = stickParent.gameObject.GetComponentInChildren<CableBody>();
            //parentLink.outAnchor = Vector3.up * 0.5f;
            cable.links[0] = parentLink;

            Cable.Link childLink = new Cable.Link();
            childLink.type = Cable.Link.LinkType.Attachment;
            childLink.body = stickChildren[0].gameObject.GetComponentInChildren<CableBody>();
            //childLink.outAnchor = Vector3.up * 0.5f;
            cable.links[1] = childLink;

            GameObject.Instantiate(cableObj);
        }
    }

    public void MoveRope()
    {
        if (stickParent != null && stickChildren.Count > 0)
        {
            if (Vector3.Magnitude(stickParent.transform.position - stickChildren[0].transform.position) < tenseDistance)
            {
                foreach (SmallBox child in stickChildren)
                {
                    child.transform.SetParent(null);
                }
            }
            else
            {
                foreach (SmallBox child in stickChildren)
                {
                    child.transform.SetParent(stickParent.transform.GetChild(0));
                }
            }
        }
    }

    public void ApplyRope()
    {
        if (stickParent != null)
        {
            foreach (SmallBox child in stickParent.GetComponentsInChildren<SmallBox>())
            {
                if (!stickChildren.Contains(child)) child.transform.SetParent(null);
            }
            foreach (SmallBox child in stickChildren)
            {
                child.transform.SetParent(stickParent.transform.GetChild(0));
            }

            if (stickChildren.Count > 0)
            {
                if (stickParent.isExtended)
                    tenseDistance = Vector3.Magnitude(stickParent.transform.position - stickChildren[0].transform.position);
                else
                    tenseDistance = Vector3.Magnitude(stickParent.transform.position - stickChildren[0].transform.position) + stickParent.range;
            }
        }
        else
        {
            foreach (SmallBox child in stickChildren)
            {
                child.transform.SetParent(null);
            }
        }
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
}
