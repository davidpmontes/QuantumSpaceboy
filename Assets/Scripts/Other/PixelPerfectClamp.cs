using UnityEngine;

public class PixelPerfectClamp : MonoBehaviour
{
    public static Vector2 GetPixelPerfectClampV2(Vector2 moveVector, float pixelsPerUnit)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }

    public static Vector3 GetPixelPerfectClampV3(Vector2 moveVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit), 0);

        return vectorInPixels / pixelsPerUnit;
    }
}