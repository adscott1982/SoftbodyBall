using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Public editable fields
    [Range(3, 100)]
    public int NumberOfSides = 4;

    [Range(0.01f, 1000f)]
    public float Radius = 1;

    // Private fields
    private List<GameObject> sides;

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

    private List<GameObject> GetSides()
    {
        var vectors = new List<Vector2>();
        var sides = new List<GameObject>();

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

        return sides;
    }

    private GameObject CreateSide(Vector2 rotatedVector, float sideLength, int index)
    {
        var sideObject = new GameObject($"Side{index}");
        sideObject.transform.parent = this.transform;

        var sideController = sideObject.AddComponent<SideController>();
        sideController.Initialise(rotatedVector, sideLength);

        return sideObject;
    }
}
