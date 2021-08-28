using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIArrow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _redDistance;
    [SerializeField] private float _greenDistance;
    [SerializeField] private Renderer _renderer;

    private List<Coin> _coins;
    private WayPoint _closestWayPoint;

    public WayPoint ClosestWayPoint => _closestWayPoint;

    private void FixedUpdate()
    {
        _closestWayPoint = GetClosestCoinDirection(_coins);
        Vector3 directionXZ = new Vector3(_closestWayPoint.Direction.x, 0, _closestWayPoint.Direction.z);
        Quaternion desiredRotation = Quaternion.LookRotation(directionXZ);
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);

        float T = Mathf.InverseLerp(_greenDistance, _redDistance, _closestWayPoint.Distance);
        _renderer.material.color = Color.Lerp(Color.green, Color.red, T);
    }

    private WayPoint GetClosestCoinDirection(List<Coin> coins)
    {
        Coin closest = coins[0];
        float closestDistance = float.MaxValue;
        coins.ForEach(coin =>
        {
            if (coin != null)
            {
                float distance = Vector3.Distance(_player.position, coin.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = coin;
                }
            }
        });

        if (closest == null)
        {
            return new WayPoint(_player.forward, 0);
        }

        return new WayPoint((closest.transform.position - _player.position).normalized, closestDistance);
    }

    public void Show(List<Coin> coins)
    {
        _coins = coins;
        enabled = true;
    }

    public void Hide()
    {
        enabled = false;
    }
}

public class WayPoint
{
    public Vector3 Direction { get; }
    public float Distance { get; }

    public WayPoint(Vector3 direction, float distance)
    {
        Direction = direction;
        Distance = distance;
    }
}