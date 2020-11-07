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
    private CapsuleCollider2D capsuleCollider;
    private HingeJoint2D hingeJoint2D;
    private Vector2 anchorOffset;
    private DistanceJoint2D distanceJoint2D;

    public Rigidbody2D RigidBody2D { get; private set; }
    public float InflationForce { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(this.end1, this.end2, Color.cyan);
    }

    private void FixedUpdate()
    {
        var vectorDirection = this.RigidBody2D.rotation.DegreeToVector2() * this.InflationForce;
        this.RigidBody2D.AddForce(vectorDirection, ForceMode2D.Force);
    }

    internal void Initialise(Vector2 rotatedVector, float sideLength, float mass, float thickness, PhysicsMaterial2D material)
    {
        this.transform.localPosition = rotatedVector;

        var perpendicularNormal = Vector2.Perpendicular(rotatedVector).normalized;
        var endOffset = perpendicularNormal * (sideLength / 2f);
        this.end1 = this.transform.position.AsVector2() - endOffset;
        this.end2 = this.transform.position.AsVector2() + endOffset;

        this.CreateRigidBody(mass, rotatedVector, thickness, material);
        this.mass = mass;
    }

    internal void ConnectJoints(Side otherSide)
    {
        this.ConnectHinge(otherSide);

    }

    private void ConnectHinge(Side otherSide)
    {
        this.hingeJoint2D = this.gameObject.AddComponent<HingeJoint2D>();
        this.hingeJoint2D.autoConfigureConnectedAnchor = false;
        this.hingeJoint2D.connectedBody = otherSide.Controller.RigidBody2D;
        this.hingeJoint2D.anchor = this.anchorOffset;
        this.hingeJoint2D.connectedAnchor = -this.anchorOffset;

        this.distanceJoint2D = this.gameObject.AddComponent<DistanceJoint2D>();
        this.distanceJoint2D.maxDistanceOnly = false;
        this.distanceJoint2D.autoConfigureConnectedAnchor = false;
        this.distanceJoint2D.autoConfigureDistance = false;
        this.distanceJoint2D.distance = 0f;
        this.distanceJoint2D.connectedBody = otherSide.Controller.RigidBody2D;
        this.distanceJoint2D.anchor = this.anchorOffset;
        this.distanceJoint2D.connectedAnchor = -this.anchorOffset;
        this.distanceJoint2D.maxDistanceOnly = false;
    }

    private void CreateRigidBody(float mass, Vector2 rotatedVector, float thickness, PhysicsMaterial2D material)
    {
        this.RigidBody2D = this.gameObject.AddComponent<Rigidbody2D>();
        
        this.RigidBody2D.mass = mass;
        this.RigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        this.RigidBody2D.gravityScale = 1f;
        this.RigidBody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        this.RigidBody2D.rotation = rotatedVector.DirectionDegrees();
        this.RigidBody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        this.RigidBody2D.sharedMaterial = material;
        this.capsuleCollider = this.gameObject.AddComponent<CapsuleCollider2D>();

        this.capsuleCollider.direction = CapsuleDirection2D.Vertical;
        var height = (this.end2 - this.end1).magnitude;
        this.anchorOffset = new Vector2(0, height / 2f);
        this.capsuleCollider.size = new Vector2(thickness, height);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;

        //if (this.name == "Side0")
        //{
        //    Gizmos.color = Color.blue;
        //}

        //Gizmos.DrawSphere(this.transform.TransformPoint(this.hingeJoint2D.anchor), 0.05f);

        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(this.transform.TransformPoint(this.RightEndOffset), 0.05f);

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawLine(this.end1, this.end2);
    }
}
