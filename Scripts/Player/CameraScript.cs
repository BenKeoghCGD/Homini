using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraScript : MonoBehaviour
{
    protected Camera _cam;
    protected GameObject _player;
    protected CameraMode mode;

    public void Start()
    {
        _cam = Camera.main;
    }

    public CameraMode getMode() { return mode; }

    public virtual void Init(GameObject player) { _player = player; }
    public abstract void Run();
}
