﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 15.0f;

    private void Update()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);    
    }
}
