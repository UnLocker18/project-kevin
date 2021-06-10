using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleThirdPRigidbodyController : MonoBehaviour
{
    [SerializeField] private Transform _cameraT;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 15f;

    [SerializeField] private float walkAnimationSpeed = 0.667f;
    [SerializeField] private float moveThreshold = 0.1f;
    
    private EnvironmentInteractions environmentInteractions;
    private ParticleSystem particleSystem;

    private Rigidbody _rigidbody;
    private Vector3 _inputVector;
    private float _inputSpeed;
    private Vector3 _targetDirection;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        environmentInteractions = GetComponent<EnvironmentInteractions>();

        if (GameObject.Find("Transformation") != null) particleSystem = GameObject.Find("Transformation").GetComponentInChildren<ParticleSystem>();
        else Debug.Log("Cannot find Transformation gameobject");

        if (environmentInteractions != null) environmentInteractions.ChangePersonality += SwitchCharacter;
    }

    void Update()
    {
        //Handle the Input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        _inputVector = new Vector3(h, 0, v);
        _inputSpeed = Mathf.Clamp(_inputVector.magnitude, 0f, 1f);

        if (_inputSpeed < moveThreshold) _inputSpeed = 0f;

        //Compute direction According to Camera Orientation
        _targetDirection = _cameraT.TransformDirection(_inputVector).normalized;
        _targetDirection.y = 0f;

        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, _targetDirection, _rotationSpeed * Time.fixedDeltaTime, 0f);

        Debug.DrawRay(transform.position + transform.up * 3f, _targetDirection * 5f, Color.red);
        Debug.DrawRay(transform.position + transform.up * 3f, newDir * 5f, Color.blue);

        _rigidbody.MoveRotation(Quaternion.LookRotation(newDir));
        _rigidbody.MovePosition(_rigidbody.position + transform.forward * _inputSpeed * _speed * Time.fixedDeltaTime);
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
