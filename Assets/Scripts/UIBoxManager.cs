using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBoxManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] TMP_Text totalItemsTxt;
    [SerializeField] BoxCounter boxCounter;

    private void Start()
    {
        scoreTxt = GetComponentsInChildren<TMP_Text>()[0];
        totalItemsTxt = GetComponentsInChildren<TMP_Text>()[1];
    }

    private void ChangeUIScore()
    {
        scoreTxt.text = "Puntaje en la caja: " + boxCounter.GetScore().ToString();
        totalItemsTxt.text = "Total items en la caja: " + boxCounter.GetTotalItems().ToString();
    }

    private void OnEnable()
    {
        boxCounter.CounterChange += ChangeUIScore;
    }

    private void OnDisable()
    {
        boxCounter.CounterChange -= ChangeUIScore;
    }
}
