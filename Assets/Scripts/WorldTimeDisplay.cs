using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WorldTimeDisplay : MonoBehaviour
{
    private TMP_Text _text;
    public int _dayCount;
    private float _timer;
    private float _interval = 30f;
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _dayCount = 0;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        int increment = Mathf.FloorToInt(_timer / _interval);
        if (increment > 0)
        {
            _timer -= increment * _interval;
            _dayCount += increment;
        }
        _text.SetText("Day: " + _dayCount);
    }
}
