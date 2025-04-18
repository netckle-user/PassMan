using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PassMan;
using DG;
using DG.Tweening;

public class Box : MonoBehaviour, IDirectionable
{
    private Enums.Direction direction = Enums.Direction.None;

    public float initMoveTime;
    public float moveSpeed;

    private void OnEnable()
    {
        if (direction == Enums.Direction.None)
            transform.DOMoveY(0, initMoveTime);
    }

    private void Update()
    {
        if (GetDirection() == Enums.Direction.None)
            return;

        transform.Translate((direction == Enums.Direction.Right? Vector3.right : Vector3.left) * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            var dir = collision.gameObject.GetComponent<IDirectionable>().GetDirection();
            if (dir == GetDirection())
            {
                Debug.Log("Collide with Character.");
                BoxSpawner.instance.ReleaseBox(this);
            }
            else
                ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        moveSpeed *= 1.5f;

        if (direction == Enums.Direction.Right)
            direction = Enums.Direction.Left;
        else
            direction = Enums.Direction.Right;
    }

    public Enums.Direction GetDirection() {  return direction; }
}
