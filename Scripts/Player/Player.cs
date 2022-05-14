using UnityEngine;
using NMaterial;

public class Player : MonoBehaviour
{
    [SerializeField] CameraMode m_CameraMode = CameraMode.FIRST;
    CameraScript[] m_CameraScripts;
    Raycasting m_Raycasting;
    PlantManager _pm;
    InventoryManager _im;

    public GameObject interactHover;

    private void Start()
    {
        interactHover.SetActive(false);
        _pm = FindObjectOfType<PlantManager>();
        _im = FindObjectOfType<InventoryManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CameraScripts = GetComponentsInChildren<CameraScript>();
        m_Raycasting = GetComponentInChildren<Raycasting>();
        foreach (CameraScript cs in m_CameraScripts)
            cs.Init(gameObject);
    }

    private void FixedUpdate()
    {
        if (Settings.gamePaused) return;

        if (m_Raycasting != null)
            m_Raycasting.Run();

        foreach (CameraScript cs in m_CameraScripts)
        {
            if (cs.getMode() == m_CameraMode)
                cs.Run();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 groundPos = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);
            _pm.GeneratePlant(groundPos);
        }

        IInteractable curObj = null;
        if (m_Raycasting.currentTarget != null)
        {
            curObj = m_Raycasting.currentTarget.GetComponent<IInteractable>();
            if (m_Raycasting.currentTarget.GetComponent<Tree>() != null && _im.currentItem != null)
                curObj.canInteract = _im.currentItem.iid == Mat.getIID(Materials.AXE);
            if (curObj.canInteract && Input.GetKeyDown(KeyCode.E))
                curObj.Interact();
        }
        interactHover.SetActive(m_Raycasting.currentTarget != null && curObj.canInteract);
    }
}

public enum CameraMode
{
    FIRST, THIRD_FORWARD, THIRD_BACKWARD
}