using UnityEngine;

public class FloorButton : MonoBehaviour
{
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

        if (hasBoxOn) _renderer.material = activeMaterial;
        else
        {
            if (activate) _renderer.material = activeMaterial;
            else _renderer.material = inactiveMaterial;
        }        
    }    
}
