using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _lowerXBound = -8f;
    private float _upperXBound = 8f;
    private float _upperYBound = 7f;
    private float _lowerYBound = -5f;

    [SerializeField]
    private float _speed = 4.0f;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private int _enemyValue = 10;

    private Collider2D _col;
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private float _deathAnimTime = 2.8f;

    [SerializeField]
    private bool _inDestructionSequence = false;

    [SerializeField]
    private AudioSource _audioSource;

    void Start()
    {
        InstantiateObjects();
    }

    void InstantiateObjects()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _anim = GetComponent<Animator>();

        _col = GetComponent<Collider2D>();

        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Enemy: Player is NULL");
        }

        if (_anim == null)
        {
            Debug.LogError("Enemy: Animator is NULL");
        }

        if (_col == null)
        {
            Debug.LogError("Enemy: Collider2D is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Enemy: AudioSource is NULL");
        }
    }

    void Update()
    {
        if (_inDestructionSequence)
        {
            return;
        }

        MoveDown();

        if (transform.position.y < _lowerYBound)
        {
            RespawnAtTop();
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void RespawnAtTop()
    {
        transform.position = new Vector3(Random.Range(_lowerXBound, _upperXBound), _upperYBound, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);

        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            DestructionSequence();
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(_enemyValue);
            }

            DestructionSequence();
        }
    }

    private void DestructionSequence()
    {   
        _inDestructionSequence = true;

        _speed = 0;

        if (_col != null)
        {
            _col.enabled = false;
        }

        if (_anim != null)
        {
            _anim.SetTrigger("OnEnemyDeath");
        }

        if (_audioSource != null)
        {
            _audioSource.Play();
        }

        Destroy(this.gameObject, _deathAnimTime);
    }
}
