using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Public editable fields
    [Range(3, 100)]
    public int NumberOfSides = 4;

    [Range(0.01f, 1000f)]
    public float Radius = 1;

    [Range(0.01f, 1000f)]
    public float MassKg = 0.45f;

    [Range(0.001f, 0.5f)]
    public float ColliderThickness = 0.01f;

    // Private fields
    private List<Side> sides;

    // Start is called before the first frame update
    private void Awake()
    {
        // Create the sides
        this.sides = this.GetSides();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private List<Side> GetSides()
    {
        var vectors = new List<Vector2>();
        var sides = new List<Side>();

        var degreesStep = 360f / this.NumberOfSides;

        for (var i = 0; i < this.NumberOfSides; i++)
        {
            // Walk round the circle
            var vector = new Vector2(this.Radius, 0);
            var rotatedVector = vector.Rotate(i * degreesStep);
            vectors.Add(rotatedVector);
        }

        // Um... kinda fluked this :) or I am a maths genius
        var tan = Mathf.Tan(degreesStep / 2f * Mathf.Deg2Rad);
        var sideLength = this.Radius * 2 * tan;

        for (var i = 0; i < this.NumberOfSides; i++)
        {
            var side = this.CreateSide(vectors[i], sideLength, i);
            sides.Add(side);
        }

        for (var i = 0; i < this.NumberOfSides; i++)
        {
            if ((i + 1) == this.NumberOfSides)
            {
                sides[i].Controller.ConnectHinge(sides[0]);
                continue;
            }

            sides[i].Controller.ConnectHinge(sides[i + 1]);
        }

        return sides;
    }

    private Side CreateSide(Vector2 rotatedVector, float sideLength, int index)
    {
        var sideObject = new GameObject($"Side{index}");
        sideObject.transform.parent = this.transform;

        var sideMass = this.MassKg / this.NumberOfSides;
        var sideController = sideObject.AddComponent<SideController>();
        sideController.Initialise(rotatedVector, sideLength, sideMass, this.ColliderThickness);

        return new Side(sideObject, sideController);
    }
}
