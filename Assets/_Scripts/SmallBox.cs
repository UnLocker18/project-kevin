using UnityEngine;

public class SmallBox : MonoBehaviour
{
    public bool isConnected = false;

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
