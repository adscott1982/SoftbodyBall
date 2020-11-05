using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int NumberOfSides = 4;
    public float Radius = 1;

    // Start is called before the first frame update
    void Awake()
    {
        // Create the sides
        var positions = this.GetPositions();
    }

    private List<Vector2> GetPositions(int v)
    {
        var origin = this.transform.position
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
