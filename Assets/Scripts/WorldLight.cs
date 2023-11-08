using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class WorldLight : MonoBehaviour
{
    private Light2D _light;
    [SerializeField] public WorldTime _worldTime;
    [SerializeField] public Gradient _gradient;
    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _worldTime.WorldTimeChanged += OnWorldTimeChanged;
    }

    private void OnWorldTimeChanged(object sender, TimeSpan e)
    {
        _light.color = _gradient.Evaluate(PercentOfDay(e));
    }

    private float PercentOfDay(TimeSpan timeSpan)
    {
        return (float)timeSpan.TotalMinutes % WorldTimeConstants.MinutesInDay / WorldTimeConstants.MinutesInDay;
    }
}
