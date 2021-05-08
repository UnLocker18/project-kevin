using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    private bool isActive = false;
    private Renderer[] _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = gameObject.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        if (isActive)
        {
            _renderer[0].material = inactiveMaterial;
        }
        else
        {
            _renderer[0].material = activeMaterial;
        }

        isActive = !isActive;
    }
}
