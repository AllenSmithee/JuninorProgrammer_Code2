﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit05
{
    public class DestroyAfterDelay : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 2);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}