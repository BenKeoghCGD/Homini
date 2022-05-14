using UnityEngine;

public class Raycasting : MonoBehaviour
{
    float pickupDistance = 10f;
    public GameObject currentTarget { get; private set;}

    public void Run()
    {
        Ray ray = GetComponent<Camera>().ViewportPointToRay(Vector3.one * 0.5f);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            bool hasHitInteractable = false;

            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                hasHitInteractable = true;
                currentTarget = hit.collider.gameObject;
            }
            else currentTarget = null;

            if (hasHitInteractable) Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            else Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickupDistance, Color.red);
        }
        else
        {
            currentTarget = null;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickupDistance, Color.yellow);
        }
    }
}