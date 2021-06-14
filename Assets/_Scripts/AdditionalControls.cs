using UnityEngine;

public class AdditionalControls : MonoBehaviour
{
    private EnvironmentInteractions environmentInteractions;
    // Start is called before the first frame update
    void Start()
    {
        environmentInteractions = gameObject.GetComponent<EnvironmentInteractions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            environmentInteractions.Interaction();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            environmentInteractions.StickRope();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            environmentInteractions.LeaveRope();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) environmentInteractions.SetPersonality(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) environmentInteractions.SetPersonality(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) environmentInteractions.SetPersonality(2);
    }
}
