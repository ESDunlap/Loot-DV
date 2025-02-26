using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onscreenDelay = 10f;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        Debug.Log("Something was hit!");
    }

    void Start()
    {
        Destroy(this.gameObject, onscreenDelay);
    }
}
