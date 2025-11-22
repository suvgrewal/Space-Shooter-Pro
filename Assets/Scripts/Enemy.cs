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

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _anim = GetComponent<Animator>();

        _col = GetComponent<Collider2D>();

        if (!_player)
        {
            Debug.LogError("Enemy: Player is NULL");
        }

        if (!_anim)
        {
            Debug.LogError("Enemy: Animator is NULL");
        }

        if (!_col)
        {
            Debug.LogError("Enemy: Collider2D is NULL");
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

            if (player)
            {
                player.Damage();
            }

            DestructionSequence();
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player)
            {
                _player.AddScore(_enemyValue);
            }

            DestructionSequence();
        }
    }

    private void DestructionSequence()
    {   
        _inDestructionSequence = true;

        if (_col)
        {
            _col.enabled = false;
        }

        if (_anim)
        {
            _anim.SetTrigger("OnEnemyDeath");
        }

        Destroy(this.gameObject, _deathAnimTime);
    }
}
