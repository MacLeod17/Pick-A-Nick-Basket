using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [Min(0)] public float gravityMax;
    public float gravity;
    public float restitution;
    public float velocityMax;
    public bool isGravityNormal = true;

    Rigidbody2D rb;
    Vector2 velocity = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameSession.Instance.State != GameSession.eState.Session) return;

        if (isGravityNormal) velocity.y += gravity;
        else velocity.x += gravity;

        if (velocity.magnitude > velocityMax) velocity = Vector3.ClampMagnitude(velocity, velocityMax);

        rb.velocity += velocity * Time.deltaTime;

        if (rb.velocity.magnitude > velocityMax) rb.velocity = Vector3.ClampMagnitude(rb.velocity, velocityMax);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance?.PlayAudio(0);
            GameSession.Instance.AddPoints(Mathf.RoundToInt(rb.velocity.magnitude));

            isGravityNormal = Random.Range(0, 2) == 0;
            gravity = Random.Range(-gravityMax, gravityMax);

            // Handle Bouncing
            rb.velocity = -rb.velocity * restitution;
            velocity = Vector2.zero;
        }
    }
}
