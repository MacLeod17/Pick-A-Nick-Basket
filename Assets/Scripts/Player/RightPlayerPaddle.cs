using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlayerPaddle : MonoBehaviour
{
    public float maxY = 0;
    public float speed = 1;

    void Update()
    {
        if (GameSession.Instance.State != GameSession.eState.Session) return;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y > -maxY)
            {
                Vector3 translate = new Vector3(0, -speed * Time.deltaTime, 0);
                transform.Translate(translate);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y < maxY)
            {
                Vector3 translate = new Vector3(0, speed * Time.deltaTime, 0);
                transform.Translate(translate);
            }
        }
    }
}
