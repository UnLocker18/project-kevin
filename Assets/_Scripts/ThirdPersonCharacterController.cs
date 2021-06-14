using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField] private Transform _cameraT;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 3f;

    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _jumpHeight = 3f;

    [SerializeField] private float walkAnimationSpeed = 0.667f;
    [SerializeField] private float moveThreshold = 0.1f;

    private CharacterController _characterController;
    private EnvironmentInteractions environmentInteractions;
    private ParticleSystem particleSystem;

    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;

    private Vector3 _velocity;
    private bool _isGrounded;    

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        environmentInteractions = GetComponent<EnvironmentInteractions>();

        if (GameObject.Find("Transformation") != null) particleSystem = GameObject.Find("Transformation").GetComponentInChildren<ParticleSystem>();
        else Debug.Log("Cannot find Transformation gameobject");

        if (environmentInteractions != null) environmentInteractions.ChangePersonality += SwitchCharacter;
    }

    
    void Update()
    {
        //Ground Check
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        //GET Input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _inputVector = new Vector3(h, 0, v);
        _inputSpeed = Mathf.Clamp(_inputVector.magnitude, 0f, 1f);

        if (_inputSpeed < moveThreshold) _inputSpeed = 0f;

        //Compute direction According to Camera Orientation
        _targetDirection = _cameraT.TransformDirection(_inputVector).normalized;
        _targetDirection.y = 0f;

        //Rotate Object
        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, _rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);        

        //Move object along forward
        _characterController.Move(transform.forward * _inputSpeed * _speed * Time.deltaTime);

        //JUMPING
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
        }

        //FALLING
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Animator animator = GetComponentInChildren<Animator>();

        animator.SetFloat("WalkSpeed", _inputSpeed * _speed * walkAnimationSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("InputSpeed", _inputSpeed, 0.1f, Time.deltaTime);
    }

    private void SwitchCharacter(int personality)
    {
        StartCoroutine("SwitchAnimation", personality);
    }

    private IEnumerator SwitchAnimation(int personality)
    {
        Animator animator = GetComponentInChildren<Animator>();

        animator.SetBool("CharacterSwitch", true);
        if (particleSystem != null) particleSystem.Play();

        yield return new WaitForSeconds(0.15f);

        transform.Find("Storico").gameObject.SetActive(personality == 0);
        transform.Find("Bambino").gameObject.SetActive(personality == 1);
        transform.Find("Sportivo").gameObject.SetActive(personality == 2);

        animator.SetBool("CharacterSwitch", false);
    }
}
