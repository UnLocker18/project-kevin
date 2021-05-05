using UnityEngine;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    private Renderer _renderer;
    private bool hasBoxOn = false;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(bool activate, bool activatorIsBox)
    {
        if (activatorIsBox) hasBoxOn = !hasBoxOn;

        if (hasBoxOn)
        {
            _renderer.material = activeMaterial;
            if (door != null) door.Open();
        }
        else
        {
            if (activate)
            {
                _renderer.material = activeMaterial;
                if (door != null) door.Open();
            }
            else
            {
                _renderer.material = inactiveMaterial;
                if (door != null) door.Close();
            }
        }        
    }    
}
