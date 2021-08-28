using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLogic : MonoBehaviour
{
    [SerializeField] private CoinsGenerator _coinsGenerator;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private UIArrow _uiArrow;

    private int _collectedCoins;
    private List<Coin> _coins;

    private void Awake()
    {
        _coins = _coinsGenerator.Generate();
        _uiArrow.Show(_coins);
        _levelUI.SetCoinsText(_collectedCoins, _coins.Count);
        _coins.ForEach(coin => coin.Collected += OnCoinCollected);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {
        _levelUI.SetDistanceText(_uiArrow.ClosestWayPoint.Distance);
    }

    private void OnCoinCollected()
    {
        _collectedCoins++;
        _levelUI.SetCoinsText(_collectedCoins, _coins.Count);
        if (_collectedCoins == _coins.Count)
        {
            Win(false);
            _uiArrow.Hide();
        }
    }

    private void Win(bool trueEnd)
    {
        _levelUI.ShowWinScreen(trueEnd);
    }
}