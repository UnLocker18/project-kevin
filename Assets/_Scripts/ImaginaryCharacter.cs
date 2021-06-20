﻿using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;

public class ImaginaryCharacter : Interactable
{
    [SerializeField] private int requiredPersonality = 1;
    [SerializeField] private float animationSeconds = 1.5f;
    [SerializeField] private float rotationSeconds = 0.5f;
    [SerializeField] private DialogueTrigger dialoguePreMove;
    [SerializeField] private DialogueTrigger dialoguePostMove;

    [SerializeField] private PathType pathType;
    [SerializeField] private PathMode pathMode;
    private List<Vector3> path = new List<Vector3>();

    private EnvironmentInteractions environmentInteractions;
    private Renderer renderer;
    private Vector3 afterInteractionPosition;
    private Vector3 afterInteractionRotation;
    private bool dialogueAlreadyTriggered = false;

    private void Awake()
    {
        environmentInteractions = GameObject.Find("MainCharacter").GetComponent<EnvironmentInteractions>();
        if (environmentInteractions != null) environmentInteractions.ChangePersonality += ToggleInteractability;
    }

    private void Start()
    {
        //_cameraT = GameObject.FindGameObjectWithTag("MainCamera").transform;

        renderer = gameObject.GetComponentInChildren<Renderer>();
        if (renderer != null) renderer.enabled = false;

        afterInteractionPosition = transform.Find("AfterInteraction").position;
        afterInteractionRotation = transform.Find("AfterInteraction").eulerAngles;

        foreach (Transform child in transform)
        {
            if (child.tag == "Path") path.Add(child.position);
        }

        Move();
    }

    //private void Update()
    //{
    //    //Compute direction According to Camera Orientation
    //    _targetDirection = _cameraT.TransformDirection(_inputVector).normalized;
    //    _targetDirection.y = 0f;

    //    //Rotate Object
    //    Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, _rotationSpeed * Time.deltaTime, 0f);
    //    transform.rotation = Quaternion.LookRotation(newDir);

    //    //Move object along forward
    //    //_characterController.Move(transform.forward * _inputSpeed * _speed * Time.deltaTime);        
    //}

    //private void Update()
    //{
    //    speed = Vector3.Distance(oldPosition, transform.position);
    //    oldPosition = transform.position;

    //    Debug.Log(GetComponent<Rigidbody>().velocity);
    //}

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
        transform.DOPath(path.ToArray(), animationSeconds, pathType, pathMode, 10).SetLookAt(0f, new Vector3(0, 0, -1));        

        //transform.DORotate(afterInteractionRotation + new Vector3(0f, 90f, 0f), rotationSeconds);
        //transform.DOMove(new Vector3(afterInteractionPosition.x, 0, afterInteractionPosition.z), animationSeconds);
    }
}
