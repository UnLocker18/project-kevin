using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Piston stickParent;

    //trasformare lista di smallbox in lista di transform?
    public List<SmallBox> stickChildren = new List<SmallBox>();
    private float tenseDistance;

    public void MoveRope()
    {
        if (stickParent != null && stickChildren.Count > 0)
        {
            Debug.Log(Vector3.Magnitude(stickParent.transform.position - stickChildren[0].transform.position));
            Debug.Log(tenseDistance);

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
