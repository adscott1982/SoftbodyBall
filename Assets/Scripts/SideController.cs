using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideController : MonoBehaviour
{
    private Vector2 end1;
    private Vector2 end2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(this.end1, this.end2, Color.cyan);
    }

    internal void Initialise(Vector2 rotatedVector, float sideLength)
    {
        this.transform.localPosition = rotatedVector;

        var perpendicularNormal = Vector2.Perpendicular(rotatedVector).normalized;
        var endOffset = perpendicularNormal * (sideLength / 2f);
        this.end1 = this.transform.position.AsVector2() - endOffset;
        this.end2 = this.transform.position.AsVector2() + endOffset;
    }


    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(this.end1, 0.05f);

        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(this.end2, 0.05f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(this.end1, this.end2);
    }
}
