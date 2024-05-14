using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningIcon : MonoBehaviour
{
    public float spinspeed = 5f;

    void Update()
    {
        transform.Rotate(0f, spinspeed, 0f);
    }
}
