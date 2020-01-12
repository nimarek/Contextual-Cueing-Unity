using UnityEngine;

/*
 * This script creates an invisible physics spherecast to switch between trials. When a participant is looking directly
 * at the inter trial fixation sphere for 1,5 seconds a new trial will start. The maximum length of the spherecast is
 * always equal to the radius of the search display. 
 */
public class GazePointer : MonoBehaviour
{
    public Spawner Spawner;
    public SubjectInput SubjectInput;
    public InterTrialInterBlock InterTrialInterBlock;
    public ColorLerper ColorLerper;
    private float sphereRadius; // Sphere size is 2x the scaling of the fixation
    public float maxDistance;
    public LayerMask layerMask;
    public GameObject currentTarget;
    private RaycastHit checkFixation;
    private Vector3 subPosition;
    private Vector3 direction;

    public void Awake()
    {
        sphereRadius = InterTrialInterBlock.scaleInterTrialFixation;
        maxDistance = Spawner.radius;
    }

    public void Update()
    {
        subPosition = transform.position;
        direction = transform.forward;

        if (Physics.SphereCast(subPosition, sphereRadius, direction, out checkFixation, maxDistance, layerMask,
            QueryTriggerInteraction.UseGlobal))
        {
            currentTarget = checkFixation.transform.gameObject;

            if (checkFixation.collider.CompareTag("InterTrialFixation"))
            {
                CheckCollision();
            }
        }
        else
        {
            currentTarget = null;
        }
    }

    private void CheckCollision()
    {
        Debug.Log("Hit it!");
        SubjectInput.EndInterTrial();
    }

    private void OnDrawGizmos()
    {
        direction = transform.forward;
        Gizmos.color = Color.red;
        Debug.DrawLine(subPosition, subPosition + direction * maxDistance);
        Gizmos.DrawWireSphere(subPosition + direction * maxDistance, sphereRadius);
    }
}