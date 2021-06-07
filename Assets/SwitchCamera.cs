using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private bool detectCamerasFromName = true;
    [SerializeField] private int from = 1;
    [SerializeField] private int to = 2;

    private CinemachineVirtualCamera fromCamera;
    private CinemachineVirtualCamera toCamera;

    private void Start()
    {
        if (detectCamerasFromName)
        {
            from = gameObject.name.Substring(gameObject.name.Length - 4)[0] - '0';
            to = gameObject.name.Substring(gameObject.name.Length - 4)[3] - '0';
        }

        Debug.Log("VirtualCamera_" + from);
        Debug.Log(to);

        fromCamera = GameObject.Find("VirtualCamera_" + from).GetComponent<CinemachineVirtualCamera>();
        toCamera = GameObject.Find("VirtualCamera_" + to).GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fromCamera.Priority = 0;
            toCamera.Priority = 1;
        }
    }
}
