using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPlayerPaddle : MonoBehaviour
{
    public float maxX = 0;
    public float speed = 1;

    void Update()
    {
        if (GameSession.Instance.State != GameSession.eState.Session) return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -maxX)
            {
                Vector3 translate = new Vector3(-speed * Time.deltaTime, 0, 0);
                transform.Translate(translate);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < maxX)
            {
                Vector3 translate = new Vector3(speed * Time.deltaTime, 0, 0);
                transform.Translate(translate);
            }
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
