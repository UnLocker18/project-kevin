using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField] private int currentPersonality = 0;

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

    [SerializeField] private RuntimeAnimatorController[] AnimatorControllers;
    //[SerializeField] private RuntimeAnimatorController bambinoAnimatorController;
    //[SerializeField] private RuntimeAnimatorController sportivoAnimatorController;
    [SerializeField] private Avatar[] Avatars;
    //[SerializeField] private Avatar bambinoAvatar;
    //[SerializeField] private Avatar sportivoAvatar;

    private CharacterController _characterController;
    private Animator animator;
    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;

    private Vector3 _velocity;
    private bool _isGrounded;    

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
        transform.Find("Storico").gameObject.SetActive(currentPersonality == 0);
        transform.Find("Bambino").gameObject.SetActive(currentPersonality == 1);
        transform.Find("Sportivo").gameObject.SetActive(currentPersonality == 2);

        animator.avatar = Avatars[currentPersonality];
        animator.runtimeAnimatorController = AnimatorControllers[currentPersonality];        

        //if (currentPersonality == 0)
        //{
        //    animator.runtimeAnimatorController = storicoAnimatorController;
        //    animator.avatar = storicoAvatar;
        //}
        //else if (currentPersonality == 1)
        //{
        //    animator.runtimeAnimatorController = bambinoAnimatorController;
        //    animator.avatar = bambinoAvatar;
        //}

        animator.SetFloat("WalkSpeed", _inputSpeed * _speed * walkAnimationSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("InputSpeed", _inputSpeed, 0.1f, Time.deltaTime);
    }
}
