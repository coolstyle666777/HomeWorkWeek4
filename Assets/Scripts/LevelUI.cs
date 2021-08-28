using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private TextMeshProUGUI _distanceText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowWinScreen(bool trueEnd)
    {
        _winPanel.SetActive(true);
        if (trueEnd)
        {
            _winText.text = "You win! Now you are free!";
        }

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void SetCoinsText(int collected, int needed)
    {
        _coinsText.text = "Coins " + collected + "/" + needed;
    }

    public void SetDistanceText(float distance)
    {
        _distanceText.text = Mathf.RoundToInt(distance) + "m";
    }
}