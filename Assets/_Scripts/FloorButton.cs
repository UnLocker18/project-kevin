using UnityEngine;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    private Renderer _renderer;    

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(bool activate)
    {
        if (activate) _renderer.material = activeMaterial;
        else _renderer.material = inactiveMaterial;
    }    
}
