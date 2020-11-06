using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideController : MonoBehaviour
{
    private Vector2 end1;
    private Vector2 end2;
    private float mass;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(this.end1, this.end2, Color.cyan);
    }

    internal void Initialise(Vector2 rotatedVector, float sideLength, float mass, float thickness)
    {
        this.transform.localPosition = rotatedVector;

        var perpendicularNormal = Vector2.Perpendicular(rotatedVector).normalized;
        var endOffset = perpendicularNormal * (sideLength / 2f);
        this.end1 = this.transform.position.AsVector2() - endOffset;
        this.end2 = this.transform.position.AsVector2() + endOffset;

        this.CreateRigidBody(mass, rotatedVector, thickness);

        this.mass = mass;
    }

    internal void ConnectHinge(Side otherSide)
    {

    }

    private void CreateRigidBody(float mass, Vector2 rotatedVector, float thickness)
    {
        this.rb = this.gameObject.AddComponent<Rigidbody2D>();
        this.rb.mass = mass;
        this.rb.bodyType = RigidbodyType2D.Dynamic;
        this.rb.gravityScale = 1f;
        this.rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        this.rb.rotation = rotatedVector.DirectionDegrees();
        this.rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        this.capsuleCollider = this.gameObject.AddComponent<CapsuleCollider2D>();

        this.capsuleCollider.direction = CapsuleDirection2D.Vertical;
        var height = (this.end2 - this.end1).magnitude;
        this.capsuleCollider.size = new Vector2(thickness, height);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(this.end1, 0.05f);

        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(this.end2, 0.05f);

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawLine(this.end1, this.end2);
    }
}
