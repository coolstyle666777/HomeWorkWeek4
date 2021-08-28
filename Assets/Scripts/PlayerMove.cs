using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _cameraCenter;
    [SerializeField] private float _torque;

    private void Start()
    {
        _rigidbody.maxAngularVelocity = 500;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddTorque(_cameraCenter.right * Input.GetAxis("Vertical") * _torque);
        _rigidbody.AddTorque(_cameraCenter.forward * Input.GetAxis("Horizontal") * -_torque);
    }
}