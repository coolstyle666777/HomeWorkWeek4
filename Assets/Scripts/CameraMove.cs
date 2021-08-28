using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Rigidbody _playerRigidBody;
    public List<Vector3> _velocitiesList;

    private void Start()
    {
        _playerRigidBody = _player.GetComponent<Rigidbody>();
        _velocitiesList = new List<Vector3>();
        for (int i = 0; i < 10; i++)
        {
            _velocitiesList.Add(_player.forward);
        }
    }

    private void FixedUpdate()
    {
        if (_playerRigidBody.velocity.magnitude > 0.5f)
        {
            _velocitiesList.Add(_playerRigidBody.velocity);
            _velocitiesList.RemoveAt(0);
        }
    }

    private void LateUpdate()
    {
        Vector3 summ = new Vector3();
        _velocitiesList.ForEach(velocity => summ += velocity);
        transform.position = _player.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(summ), Time.deltaTime * 10f);
    }
}