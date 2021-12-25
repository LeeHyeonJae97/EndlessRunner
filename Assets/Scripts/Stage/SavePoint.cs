using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private Color _savedColor = Color.white;
    [SerializeField] private Color _defaultColor = Color.white;

    private SpriteRenderer _sr;
    private ParticleSystem _ps;
    private bool _saved;

    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        _sr.color = _defaultColor;
    }

    public void Save(bool value)
    {
        if (_saved == value) return;

        _saved = value;
        _sr.color = _saved ? _savedColor : _defaultColor;
        if (_saved) _ps.Play();
    }
}
