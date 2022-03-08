using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] CameraMode m_CameraMode = CameraMode.FIRST;
    CameraScript[] m_CameraScripts;
    Raycasting m_Raycasting;
    PlantManager _pm;

    private void Start()
    {
        _pm = FindObjectOfType<PlantManager>();
        Inventory.invSlots = Inventory.capacity = 7;
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

        /*if (Input.GetKeyDown(KeyCode.C))
            m_CameraMode = (int)m_CameraMode == 2 ? 0 : m_CameraMode + 1;*/

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
            Debug.Log("Logging");
            Vector3 groundPos = new Vector3(transform.position.x, Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z);
            _pm.GeneratePlant(groundPos);
        }

        if (m_Raycasting.currentTarget != null)
        {
            if(m_Raycasting.currentTarget.GetComponent<I_Tree>() != null)
            {
                // LOOKING AT TREE
                if (Input.GetKeyDown(KeyCode.E)) _pm.HarvestTree(m_Raycasting.currentTarget);
            }
        }
    }
}

public enum CameraMode
{
    FIRST, THIRD_FORWARD, THIRD_BACKWARD
}