using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera from;
    [SerializeField] private CinemachineVirtualCamera to;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            from.Priority = 0;
            to.Priority = 1;
        }
    }
}
