using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraScript : MonoBehaviour
{
    public static Camera _cam;
    public static GameObject _player;
    public static CameraMode mode;

    public void Start()
    {
        _cam = Camera.main;
    }

    public CameraMode getMode() { return mode; }

    public abstract void Init(GameObject player);
    public abstract void Run();
}
