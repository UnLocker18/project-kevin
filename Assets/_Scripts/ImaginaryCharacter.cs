using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class ImaginaryCharacter : Interactable
{
    [SerializeField] private bool westernVersion = false;
    [SerializeField] private int requiredPersonality = 1;
    [SerializeField] private float animationSeconds = 1.5f;
    [SerializeField] private DialogueTrigger dialoguePreMove;
    [SerializeField] private DialogueTrigger dialoguePostMove;
    [SerializeField] private bool collidersActiveOnMove = true;

    [SerializeField] private PathType pathType;
    [SerializeField] private PathMode pathMode;
    [SerializeField] private Ease pathEase;
    [SerializeField] private Ease floatEase;
    private List<Vector3> path = new List<Vector3>();

    private EnvironmentInteractions environmentInteractions;
    private Renderer renderer;
    private Animator animator;
    private Collider[] colliders;
    
    private bool dialogueAlreadyTriggered = false;
    private Vector3 mLastPosition;
    private float walkSpeedCoeff = 1f;
    
    private bool hasMoved = false;

    private void Awake()
    {
        environmentInteractions = GameObject.Find("MainCharacter").GetComponent<EnvironmentInteractions>();
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleInteractability;
    }

    private void Start()
    {
        animator = transform.GetChild(0).GetComponentInChildren<Animator>();

        renderer = GetComponentInChildren<Renderer>();
        if (renderer != null) renderer.enabled = false;

        foreach (Transform child in transform)
        {
            if (child.tag == "Path")
            {
                path.Add(child.position);
            }
        }

        if (westernVersion) colliders = transform.GetChild(0).GetComponents<BoxCollider>();

        FloatAnimation();
    }

    private void Update()
    {
        if (westernVersion) return;

        float speed = (transform.position - mLastPosition).magnitude / Time.deltaTime;
        mLastPosition = transform.position;

        animator.SetFloat("WalkSpeed", speed * walkSpeedCoeff, 0.1f, Time.deltaTime);
    }

    private void ToggleInteractability(int personality)
    {
        if (renderer == null) return;

        if (personality == requiredPersonality)
        {
            renderer.enabled = true;
            isInteractable = true;
        }
        else
        {
            renderer.enabled = false;
            isInteractable = false;
        }
    }

    public override int Interact(Transform mainCharacter)
    {
        if (!isInteractable) return -1;

        if (!dialogueAlreadyTriggered && dialoguePreMove != null)
        {
            dialoguePreMove.TriggerTalk(this);
            dialogueAlreadyTriggered = true;
        }
        else if (dialoguePostMove != null) dialoguePostMove.TriggerDialogue(-1);

        return -1;
    }

    public void Move()
    {
        if (hasMoved) return;

        if (!collidersActiveOnMove && colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                col.enabled = false;
                environmentInteractions.gameObject.GetComponentInChildren<Trigger>().RemoveFromList(col);
            }            
        }

        DOTween.Kill(transform);
        transform.DOPath(path.ToArray(), animationSeconds, pathType, pathMode, 10)
            .SetOptions(AxisConstraint.None, AxisConstraint.X | AxisConstraint.Z)
            .SetEase(pathEase).SetLookAt(0f, new Vector3(0, 0, -1))
            .OnComplete( () => {
                FloatAnimation();
                if (!collidersActiveOnMove && colliders.Length > 0)
                {
                    foreach (Collider col in colliders)
                    {
                        col.enabled = true;
                    }
                }
            });

        hasMoved = true;        
    }

    private void FloatAnimation()
    {
        if (westernVersion)
        {
            Vector3[] floatPath = new Vector3[4];
            floatPath[0] = transform.localPosition;
            floatPath[1] = transform.localPosition + new Vector3(0f, 0.05f, -0.05f);
            floatPath[2] = transform.localPosition + new Vector3(0f, 0.1f, 0f);
            floatPath[3] = transform.localPosition + new Vector3(0f, 0.05f, 0.05f);

            transform.DOLocalPath(floatPath, 8f, pathType, pathMode, 10).SetOptions(true).SetEase(floatEase).SetLoops(-1);
        }
    }
}
