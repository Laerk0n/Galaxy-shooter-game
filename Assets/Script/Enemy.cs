﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private GameObject _enemyPrefab;
    private float _respawntime;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;



    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        
        if (_player == null)
                {
            Debug.LogError("The Player is NULL");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(8.0f, -8.0f), 7 , 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     if (other.tag == "Player")
        {
            other.transform.GetComponent<Player>().Damage();
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.3f;
            Destroy(this.gameObject, 2.3f);

            _audioSource.Play();
        }
     if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5 ,10));
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0.3f;
            Destroy(this.gameObject, 2.3f);

            _audioSource.Play();
        }
    }
}
