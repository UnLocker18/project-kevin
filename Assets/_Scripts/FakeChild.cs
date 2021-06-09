using UnityEngine;

public class FakeChild : MonoBehaviour
{
    [SerializeField] private Transform[] FakeParents;

    private Transform FakeParent;
    private Transform _thisChild;
    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;

    private void Start()
    {
        _thisChild = transform;

        foreach (Transform fp in FakeParents)
        {
            if (fp.gameObject.activeSelf)
            {
                SetFakeParent(fp);
            }
        }
        
    }

    private void Update()
    {
        foreach (Transform fp in FakeParents)
        {
            if (fp.gameObject.activeSelf)
            {
                FakeParent = fp;
            }
        }

        if (FakeParent == null)
            return;

        // Position the object on top of the parent
        _thisChild.position = FakeParent.position;
        // Set the rotation based on the parent and stored offset rotation
        _thisChild.rotation = FakeParent.rotation * _rotationOffset;
        // Move the child back to the reference location
        _thisChild.Translate(_positionOffset);
    }

    public void SetFakeParent(Transform parent)
    {
        //Offset vector
        _positionOffset = _thisChild.InverseTransformPoint(_thisChild.position) - _thisChild.InverseTransformPoint(parent.position);
        //Offset rotation
        _rotationOffset = Quaternion.Inverse(parent.rotation) * transform.rotation;
        //Our fake parent
        FakeParent = parent;
    }
}
