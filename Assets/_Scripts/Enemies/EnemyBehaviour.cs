using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D hitBox;

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] LayerMask terrain;

    short direction = 1;

    Vector3 lscale;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        lscale = transform.localScale;
    }

    void Update()
    {
        direction = getMovementDirection();

        CheckForWall();
        rb.velocity = new Vector2(direction * moveSpeed, 0f);
    }

    private void CheckForWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(hitBox.bounds.center, hitBox.size / 2f, 0f, Vector2.right * direction, hitBox.size.x / 4f + moveSpeed * Time.deltaTime, terrain);
        if (hit.collider)
        {
            transform.localScale = new Vector3(-(Mathf.Sign(rb.velocity.x)), lscale.y, lscale.z);
        }
    }

    private short getMovementDirection()
    {
        return (short)Mathf.Sign(transform.localScale.x);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector3(-(Mathf.Sign(rb.velocity.x)), lscale.y, lscale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            moveSpeed = 0f;
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponents<BoxCollider2D>()[0].enabled = false;
            GetComponents<BoxCollider2D>()[1].enabled = false;
        }
    }
}
