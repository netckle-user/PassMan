using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PassMan;

public class Portal : MonoBehaviour, IDirectionable
{
    public Transform shootPoint; 
    public Portal pairPortal;

    public Enums.Direction direction = Enums.Direction.None;
    public Enums.Direction GetDirection() { return direction; }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box" || 
            collision.gameObject.tag == "OverBox")
        {
            var dir = collision.gameObject.GetComponent<IDirectionable>().GetDirection();
            if (dir == GetDirection())
                return;           
            else
                collision.gameObject.transform.position = pairPortal.shootPoint.position;            
        }
    }
}
