using UnityEngine;

public class Compass : MonoBehaviour
{
    Vector3 _startPos;
    float angleToPixel;
    GameObject _player;

    private void Start()
    {
        _startPos = transform.position;
        angleToPixel = (Constants.CompassWidth / 360f) / 2f;
        _player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        Vector3 p = Vector3.Cross(Vector3.forward, _player.transform.forward);
        float dir = Vector3.Dot(p, Vector3.up);
        transform.position = _startPos + (new Vector3(Vector3.Angle(_player.transform.forward, Vector3.forward) * Mathf.Sign(dir) * angleToPixel, 0, 0));
    }
}