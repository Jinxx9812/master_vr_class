using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] TMP_Text totalItemsTxt;
    [SerializeField] private BoxCounter blueCounter;

    private void ChangeUIScore()
    {
        scoreTxt.text = "Puntaje en la caja: " + blueCounter.GetScore().ToString();
        totalItemsTxt.text = "Total items en la caja: " + blueCounter.GetTotalItems().ToString();
    }

    private void OnEnable()
    {
        blueCounter.CounterChange += ChangeUIScore;
    }

    private void OnDisable()
    {
        blueCounter.CounterChange -= ChangeUIScore;
    }
}
