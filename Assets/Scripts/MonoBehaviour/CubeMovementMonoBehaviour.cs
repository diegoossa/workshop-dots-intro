using UnityEngine;

public class CubeMovementMonoBehaviour : MonoBehaviour
{
    [Header("Cube Movement Settings")]
    [Tooltip("How fast the cube rotates (radians per second).")]
    public float speed = 1.0f;

    [Tooltip("Scale factor for the movement path.")]
    public float scale = 3.0f;

    // Store the initial position from the scene.
    Vector3 m_InitialPosition;
    float m_AngleOffset;

    void Start()
    {
        m_InitialPosition = transform.position;
        m_AngleOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // Use Time.time to simulate the elapsed time.
        float time = Time.time;

        // Calculate the polar angle θ with the cube's speed and angleOffset.
        float theta = time * speed + m_AngleOffset;

        // Use the provided equation to compute the radius: r = sin(4θ)
        float r = Mathf.Sin(4.0f * theta);

        // Convert polar coordinates into Cartesian and apply the scale factor.
        float offsetX = scale * r * Mathf.Cos(theta);
        float offsetZ = scale * r * Mathf.Sin(theta);

        // Add the computed offset to the initial position.
        Vector3 newPosition = m_InitialPosition + new Vector3(offsetX, 0f, offsetZ);

        // Set the cube's new position.
        transform.position = newPosition;
    }
}
