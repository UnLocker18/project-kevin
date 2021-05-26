using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Obi;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject ropeMesh;
    public Piston stickParent;

    //trasformare lista di smallbox in lista di transform?
    public List<SmallBox> stickChildren = new List<SmallBox>();
    private float tenseDistance;

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

                GenerateMesh();
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

    private void GenerateMesh()
    {
        if (ropeMesh != null && stickParent != null && stickChildren.Count > 0)
        {
            Vector3 parentPosition = stickParent.transform.GetChild(0).position;
            Vector3 childPosition = stickChildren[0].transform.position; // + new Vector3(0, 0.25f, 0);

            ObiRope obiRope = ropeMesh.GetComponentInChildren<ObiRope>();
            //ObiRopeBlueprint blueprint = ScriptableObject.CreateInstance<ObiRopeBlueprint>();

            //// Procedurally generate the rope path (a simple straight line):
            //blueprint.path.Clear();
            //blueprint.path.AddControlPoint(parentPosition, -Vector3.right, Vector3.right, Vector3.up, 0.1f, 0.1f, 1, 1, Color.white, "start");
            //blueprint.path.AddControlPoint(childPosition, -Vector3.right, Vector3.right, Vector3.up, 0.1f, 0.1f, 1, 1, Color.white, "end");
            //blueprint.path.FlushEvents();

            //// generate the particle representation of the rope (wait until it has finished):
            //StartCoroutine(Generate(blueprint));

            ObiRopeBlueprint blueprint = obiRope.ropeBlueprint;

            blueprint.path.Clear();
            blueprint.path.AddControlPoint(parentPosition, -Vector3.right, Vector3.right, Vector3.up, 0.1f, 0.1f, 1, 1, Color.white, "start");
            blueprint.path.AddControlPoint(childPosition, -Vector3.right, Vector3.right, Vector3.up, 0.1f, 0.1f, 1, 1, Color.white, "end");
            blueprint.path.FlushEvents();

            StartCoroutine(Generate(blueprint));

            obiRope.ropeBlueprint = blueprint;

            ropeMesh.GetComponentsInChildren<ObiParticleAttachment>()[0].target = stickParent.transform.GetChild(0);
            ropeMesh.GetComponentsInChildren<ObiParticleAttachment>()[0].particleGroup = blueprint.groups[0];

            ropeMesh.GetComponentsInChildren<ObiParticleAttachment>()[1].target = stickChildren[0].transform;
            ropeMesh.GetComponentsInChildren<ObiParticleAttachment>()[1].particleGroup = blueprint.groups[1];

            Instantiate(ropeMesh, Vector3.zero, Quaternion.identity);
        }
    }

    IEnumerator Generate(ObiRopeBlueprint blueprint)
    {
        yield return blueprint.Generate();
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
