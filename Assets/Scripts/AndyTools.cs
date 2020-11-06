using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AndyTools
{
    public const float FloatEqualityTolerance = 0.000001f;

    public static Vector3 AddVector2(this Vector3 vector3, Vector2 vector2)
    {
        return new Vector3(vector3.x + vector2.x, vector3.y + vector2.y);
    }

    public static float DirectionDegrees(this Vector2 vector)
    {
        return (Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
    }

    public static float RelativeDirectionDegrees(this Vector2 vector, Vector2 relativeVector)
    {
        // Calculate the angle of the relative vector
        var relativeVectorAngle = relativeVector.DirectionDegrees();

        // Rotate the incoming vector by the angle difference
        var rotatedVector = vector.Rotate(-relativeVectorAngle);

        // return the angle
        return (Mathf.Atan2(rotatedVector.y, rotatedVector.x) * Mathf.Rad2Deg);
    }

    // Get the direction from this position to the destination position in 2D space
    public static Vector2 NormalizedDirectionVector2To(this Vector3 originPosition, Vector3 destination)
    {
        var vectorToOrigin = destination.AsVector2() - originPosition.AsVector2();
        return vectorToOrigin.normalized;
    }

    public static Vector2 Rotate(this Vector2 vector, float degrees)
    {
        var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        var tx = vector.x;
        var ty = vector.y;
        vector.x = (cos * tx) - (sin * ty);
        vector.y = (sin * tx) + (cos * ty);
        return vector;
    }

    public static bool IsInRange(this float value, float firstLimit, float secondLimit)
    {
        if (value >= firstLimit && value <= secondLimit)
        {
            return true;
        }

        return value >= secondLimit && value <= firstLimit;
    }

    public static float GetLerpedRotationDelta(float currentRotation, float targetRotation, float interpolationValue, float maxRotationSpeed)
    {
        var lerpedRotation = Mathf.LerpAngle(currentRotation, targetRotation, interpolationValue);
        var rotationDelta = lerpedRotation - currentRotation;

        if (Mathf.Abs(rotationDelta) > maxRotationSpeed)
        {
            rotationDelta = Mathf.Sign(rotationDelta) * maxRotationSpeed;
        }

        return rotationDelta;
    }

    public static Quaternion AsEulerZ(this float zRotation)
    {
        return Quaternion.Euler(new Vector3(0, 0, zRotation));
    }

    public static Vector2 RadianToVector2(this float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(this float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    public static Bounds OrthographicBounds(this Camera camera)
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }

    public static Rect OrthographicRectInWorldSpace(this Camera camera)
    {
        var height = camera.orthographicSize * 2;
        var width = height * Screen.width / Screen.height;
        var x = camera.transform.position.x - (width / 2);
        var y = camera.transform.position.y - (height / 2);

        return new Rect(x, y, width, height);
    }

    public static Vector2 AsVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }

    public static Vector3 AsVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0f);
    }

    public static Vector2? ToVector2(this float[] floatArray)
    {
        Vector2? returnVector = null;
        return floatArray == null ? returnVector : new Vector2(floatArray[0], floatArray[1]);
    }

    public static float[] ToFloatArray(this Vector2? vector2)
    {
        return vector2.HasValue ? new[] { vector2.Value.x, vector2.Value.y } : null;
    }

    public static Vector3? ToVector3(this float[] floatArray)
    {
        Vector3? returnVector = null;
        return floatArray == null ? returnVector : new Vector3(floatArray[0], floatArray[1], floatArray[2]);
    }

    public static float[] ToFloatArray(this Vector3? vector3)
    {
        return vector3.HasValue ? new[] { vector3.Value.x, vector3.Value.y, vector3.Value.z } : null;
    }

    public static Color ChangeAlpha(this Color color, float newAlpha)
    {
        return new Color(color.r, color.g, color.b, newAlpha);
    }

    public static List<GameObject> FindAllObjectsInScene(string matchingTag = null)
    {
        var activeScene = SceneManager.GetActiveScene();

        var rootObjects = activeScene.GetRootGameObjects();

        var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        var objectsInScene = new List<GameObject>();

        for (int i = 0; i < rootObjects.Length; i++)
        {
            objectsInScene.Add(rootObjects[i]);
        }

        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].transform.root)
            {
                for (int i2 = 0; i2 < rootObjects.Length; i2++)
                {
                    if (allObjects[i].transform.root == rootObjects[i2].transform && allObjects[i] != rootObjects[i2])
                    {
                        objectsInScene.Add(allObjects[i]);
                        break;
                    }
                }
            }
        }

        return matchingTag == null ? objectsInScene : objectsInScene.Where(o => o.tag == matchingTag).ToList();
    }
}
