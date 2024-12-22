using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform anchor; // Point around which the cannon rotates
    public float rotationSpeed = 720f; // Rotation speed in degrees per second

    private Transform target; // Current target the cannon is aiming at

    // Called by DetectEnemy to set the target
    public void AimAt(Transform enemyTarget)
    {
        target = enemyTarget;
    }

    void Update()
    {
        // Do nothing if there's no target
        if (target == null) return;

        // Calculate the direction to the target
        Vector3 direction = target.position - anchor.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Get the current angle of the cannon
        float currentAngle = transform.eulerAngles.z;

        // Calculate the shortest rotation direction
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        // Determine rotation step for this frame
        float rotationStep = rotationSpeed * Time.deltaTime;

        // Rotate toward the target
        if (Mathf.Abs(angleDifference) <= rotationStep)
        {
            // Snap to the target angle if close enough
            transform.eulerAngles = new Vector3(0, 0, targetAngle);
        }
        else
        {
            // Rotate in the shortest direction toward the target
            float rotationDirection = Mathf.Sign(angleDifference); // +1 or -1
            transform.eulerAngles += new Vector3(0, 0, rotationDirection * rotationStep);
        }
    }
}
