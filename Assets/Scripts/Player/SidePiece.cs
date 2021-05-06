using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePiece : MonoBehaviour
{
    Vector3 position;

    void Start()
    {
        position = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    void Update()
    {
        transform.localPosition = position;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
