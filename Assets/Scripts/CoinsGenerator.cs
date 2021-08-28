using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _coinsCount;
    [SerializeField] private float _boundSize;

    private float _radius;

    public List<Coin> Generate()
    {
        _radius = _coinPrefab.GetComponentInChildren<SphereCollider>().radius;
        RaycastHit hit;
        List<Coin> coins = new List<Coin>();
        for (int i = 0; i < _coinsCount; i++)
        {
            while (true)
            {
                Physics.SphereCast(
                    new Vector3(Random.Range(-_boundSize, _boundSize), 100, Random.Range(-_boundSize, _boundSize)),
                    _radius,
                    Vector3.down, out hit, float.MaxValue);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        GameObject coin = Instantiate(_coinPrefab,
                            hit.point,
                            Quaternion.identity);

                        coins.Add(coin.GetComponentInChildren<Coin>());
                        break;
                    }
                }
            }
        }

        return coins;
    }
}