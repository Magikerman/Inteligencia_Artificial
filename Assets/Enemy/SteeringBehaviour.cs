using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public static class SteeringBehaviour
{
    public static Vector3 GetRotation(Transform self, Transform player)
    {
        Vector3 dir = (player.position - self.position).normalized;
        return dir;
    }
}
