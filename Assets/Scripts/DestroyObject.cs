using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float _time;

    private void Awake()
    {
        Destroy(gameObject, _time);
    }
}
