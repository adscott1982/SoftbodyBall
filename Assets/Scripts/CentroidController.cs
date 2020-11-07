using UnityEngine;

public class CentroidController : MonoBehaviour
{
    public Rigidbody2D RigidBody2D { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Initialise(float centroidMass)
    {
        this.RigidBody2D = this.gameObject.AddComponent<Rigidbody2D>();

        this.RigidBody2D.mass = centroidMass;
        this.RigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        this.RigidBody2D.gravityScale = 1f;
        this.RigidBody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        this.RigidBody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.transform.position, 0.05f);
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
