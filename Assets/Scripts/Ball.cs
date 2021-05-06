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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            AudioManager.Instance?.PlayAudio(0);
            GameSession.Instance.AddPoints(Mathf.RoundToInt(rb.velocity.magnitude));

            isGravityNormal = Random.Range(0, 2) == 0;
            gravity = Random.Range(-gravityMax, gravityMax);

            // Handle Bouncing
            CheckBounce(collision);
            velocity = Vector2.zero;
        }
    }

    private void CheckBounce(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Horizontal"))
        {
            if (Mathf.Abs(collision.collider.bounds.max.y) < Mathf.Abs(transform.position.y))
            {
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y) * restitution;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y) * restitution;
            }
        }
        else
        {
            if (Mathf.Abs(collision.collider.bounds.max.x) < Mathf.Abs(transform.position.x))
            {
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y) * restitution;
            }
            else
            {
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y) * restitution;
            }
        }
    }
}
