using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public float pickupDistance = 100f;
    public GameObject currentTarget = null;

    public void Run()
    {
        Ray ray = GetComponent<Camera>().ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            bool hasHitInteractable = false;

            if (hit.collider.tag == "Interactable")
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