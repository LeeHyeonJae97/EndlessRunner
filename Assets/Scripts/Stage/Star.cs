using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private Color _gainedColor = Color.white;
    [SerializeField] private Color _defaultColor = Color.white;

    private SpriteRenderer _sr;
    private ParticleSystem _ps;
    private bool _gained;

    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        _gained = false;
        _sr.color = _defaultColor;
    }

    public void Gain(bool value)
    {
        if (_gained == value) return;

        _gained = value;
        _sr.color = _gained ? _gainedColor : _defaultColor;
        if (_gained) _ps.Play();
    }
}
