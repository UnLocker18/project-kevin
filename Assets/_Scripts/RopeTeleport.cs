using System.Collections;
using UnityEngine;

public class RopeTeleport : MonoBehaviour
{
    [SerializeField] private bool isColliding = false;
    [SerializeField] private int angleIncrement = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Trigger" && other.name != "MainCharacter") isColliding = true;    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name != "Trigger" && other.name != "MainCharacter") isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name != "Trigger" && other.name != "MainCharacter") isColliding = false;
    }
    
    public void Teleport(Transform rope)
    {
        if (!isColliding)
        {
            rope.position = transform.GetChild(0).position;
            transform.localEulerAngles = Vector3.zero;
        }
        else StartCoroutine("AvoidCollision", rope);
    }

    private IEnumerator AvoidCollision(Transform rope)
    {        
        while (isColliding)
        {
            for (int i = 0; i < 360; i += angleIncrement)
            {
                yield return new WaitForEndOfFrame();
                transform.Rotate(new Vector3(0, i, 0));

                if (!isColliding)
                {
                    rope.position = transform.GetChild(0).position;
                    break;
                }
            }
        }        

        transform.localEulerAngles = Vector3.zero;
    }
}
