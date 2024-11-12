using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    public Transform trans;

    [Header("Stats")]
    [Tooltip("How many units the projectile will move forward per second.")]
    public float speed = 34;
    [Tooltip("The distance the projectile will travel before it comes to a stop.")]
    public float range = 70;

    [Tooltip("Bounciness of the projectile upon hitting walls.")]
    public float bounceFactor = 0.8f;
    private Rigidbody rb;
    private Vector3 spawnPoint;

    void Start()
    {
        spawnPoint = trans.position;

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Disable gravity if the projectile shouldn't be affected by it
        rb.useGravity = false;

        // Freeze Y-axis position to prevent vertical movement
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        // Set the initial velocity of the projectile
        rb.velocity = trans.forward * speed;
    }

    void Update()
    {
        // Move the projectile along its local Z axis (forward):
        // trans.Translate(0, 0, speed * Time.deltaTime, Space.Self);

        // Destroy the projectile if it has traveled to or past its range:
        if (Vector3.Distance(trans.position, spawnPoint) >= range)
        {
            Destroy(gameObject);
        }
    }

    // This is called when the projectile collides with something
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        // Check if the projectile hit a wall
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Wall wall = collision.gameObject.GetComponent<Wall>();
            if (wall != null)
            {
                wall.OnHit();  // Trigger visual effect on the wall
            }
            // Reflect the velocity based on the collision's normal and apply the bounce factor
            Vector3 reflectDir = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);

            // Log the reflected velocity before adjustments
            Debug.Log("Reflected velocity before adjustments: " + reflectDir);

            // Ensure both X and Z velocities are positive
            reflectDir.x = Mathf.Abs(reflectDir.x); // Force positive X velocity
            reflectDir.z = Mathf.Abs(reflectDir.z); // Force positive Z velocity

            // Apply the reflected and adjusted velocity with the bounce factor
            rb.velocity = reflectDir * bounceFactor;

            // Force Y velocity to zero
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // Log the final velocity for debugging
            Debug.Log("Projectile velocity after bounce: " + rb.velocity);
        }
    }
}

