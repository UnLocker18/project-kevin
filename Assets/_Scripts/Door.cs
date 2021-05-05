using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        transform.DORotate(new Vector3(0, 120, 0), 1);
    }

    public void Close()
    {
        transform.DORotate(new Vector3(0, 0, 0), 1);
    }
}
