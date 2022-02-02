using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CameraMode m_CameraMode = CameraMode.FIRST;
    CameraScript[] m_CameraScripts;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_CameraScripts = GetComponentsInChildren<CameraScript>();
        foreach (CameraScript cs in m_CameraScripts) cs.Init(gameObject);
    }

    private void Update()
    {
        if (Settings.gamePaused) return;
        if (Input.GetKeyDown(KeyCode.C)) m_CameraMode = (int)m_CameraMode == 2 ? 0 : m_CameraMode + 1;

        foreach (CameraScript cs in m_CameraScripts) if (cs.getMode() == m_CameraMode) cs.Run();
    }
}

public enum CameraMode
{
    FIRST, THIRD_FORWARD, THIRD_BACKWARD
}