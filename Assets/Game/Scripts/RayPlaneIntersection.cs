using UnityEngine;

public class RayPlaneIntersection
{
    /// <summary>
    /// Calculates the intersection point between a ray and a horizontal plane.
    /// </summary>
    /// <param name="planeY">The y-coordinate of the horizontal plane.</param>
    /// <param name="ray">The ray defined by its origin and direction.</param>
    /// <returns>The intersection point as a Vector3. If no intersection, returns Vector3.zero.</returns>
    public static Vector3 CalculateIntersection(float planeY, Ray ray)
    {
        // Ensure the ray is not parallel to the plane
        if (ray.direction.y == 0)
        {
            // No intersection if the ray is parallel to the plane
            return Vector3.zero;
        }

        // Calculate the distance t at which the ray intersects the plane
        float t = (planeY - ray.origin.y) / ray.direction.y;

        // Check if t is negative which means the intersection is behind the ray's origin
        if (t < 0)
        {
            // No intersection if the intersection point is behind the ray's origin
            return Vector3.zero;
        }
        
        // Calculate the intersection point
        Vector3 intersectionPoint = ray.origin + t * ray.direction;

        return intersectionPoint;
    }
}