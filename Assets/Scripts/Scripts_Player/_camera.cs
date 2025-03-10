using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _camera : MonoBehaviour
{
    Camera _main;
    [SerializeReference] public Transform _player;
    public Vector3 _offset;


    void Awake()
    {
        _main = Camera.main;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _player.position + _offset, 1f );

    }
}
