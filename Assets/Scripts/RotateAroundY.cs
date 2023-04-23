using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundY : MonoBehaviour
{
   [SerializeField] float angle;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, angle * Time.deltaTime , 0f);
    }
}
