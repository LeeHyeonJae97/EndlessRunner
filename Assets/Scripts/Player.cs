using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extension;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _destroyY;
    [SerializeField] private GameObject _destroyEffect;
    [SerializeField] private PlayEventChannelSO _channel;

    public Difficulty Difficulty
    {
        set
        {
            switch (value)
            {
                case Difficulty.Beginner:
                    _gameSpeed = 1f;
                    break;

                case Difficulty.Expert:
                    _gameSpeed = 1.3f;
                    break;
            }
        }
    }
    public int Star => _stars.Count;

    private Rigidbody _rb;
    private Collider _coll;
    private Transform _mesh;
    private List<Star> _stars;
    private Vector3 _moveDir;
    private GameObject _stuff;
    private bool _tappable;
    private Vector3 _savedPos;
    private Vector3 _savedDir;
    private int _savedStarCount;
    private float _gameSpeed;

    public UnityAction<int> onStarValueChanged;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _coll = GetComponentInChildren<Collider>();
        _mesh = transform.GetChild(0);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InputPanel.onClick += Tap;
    }

    private void OnDisable()
    {
        InputPanel.onClick -= Tap;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Stuff"))
        {
            if (other.gameObject.CompareTag("Goal"))
            {
                _channel.OnGameCleared();
                return;
            }

            if (_stuff == null) TimeExtension.Slow();
            _stuff = other.gameObject;
            _tappable = true;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Stuff") && _stuff == other.gameObject)
        {
            TimeExtension.Fast();
            _stuff = null;
            _tappable = false;
        }
    }

    private void Update()
    {
        // check falling
        if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, 10f, 1 << LayerMask.NameToLayer("Track")) && transform.position.y <= _destroyY)
            Destroy();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDir * _moveSpeed * _gameSpeed * Time.fixedDeltaTime);
    }

    public void Ready(Vector3 initialDir, Vector3 initialPos)
    {
        enabled = false;
        _coll.enabled = false;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _moveDir = initialDir.normalized;
        transform.position = initialPos;
        transform.rotation = Quaternion.identity;
        _stars = new List<Star>();
        onStarValueChanged?.Invoke(0);
        _stuff = null;
        _savedStarCount = 0;
        _savedDir = initialDir;
        _savedPos = initialPos;
        gameObject.SetActive(true);
    }

    public void Ready()
    {
        enabled = false;
        _coll.enabled = false;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _moveDir = _savedDir;
        transform.position = _savedPos;
        transform.rotation = Quaternion.identity;
        for (int i = 0; i < _stars.Count;)
        {
            if (i < _savedStarCount)
            {
                i++;
            }
            else
            {
                _stars[i].Gain(false);
                _stars.RemoveAt(i);
            }
        }
        onStarValueChanged?.Invoke(_stars.Count);
        _stuff = null;
        gameObject.SetActive(true);
    }

    public void Go()
    {
        enabled = true;
        _coll.enabled = true;
        _rb.useGravity = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Tap()
    {
        if (_stuff == null || !_tappable) return;

        switch (_stuff.tag)
        {
            case "Jumper":
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _mesh.DOKill(true);
                Vector3 euler = _mesh.rotation.eulerAngles;
                euler.y += 90;
                _mesh.DORotate(euler, 0.25f, RotateMode.Fast);
                break;

            case "DirectionChanger":
                _moveDir = _stuff.GetComponent<DirectionChanger>().Direction.normalized;
                break;

            case "Portal":
                transform.position = _stuff.GetComponent<Portal>().WarpedPosition;
                break;

            case "Star":
                Star star = _stuff.GetComponent<Star>();
                star.Gain(true);
                _stars.Add(star);
                onStarValueChanged?.Invoke(_stars.Count);
                break;

            case "SavePoint":
                _stuff.GetComponent<SavePoint>().Save(true);
                _savedPos = _stuff.transform.position;
                _savedDir = _moveDir;
                _savedStarCount = _stars.Count;
                break;
        }
        _tappable = false;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);

        _destroyEffect.transform.position = gameObject.transform.position;
        _destroyEffect.gameObject.SetActive(true);

        _channel.OnGameOver();
    }
}
