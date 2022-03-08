using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TickTimer : MonoBehaviour
{
    int _TickDuration = 10; // In seconds
    public UnityEvent TickEvent;

    public IEnumerator beginTickTimer()
    {
        if (TickEvent == null) TickEvent = new UnityEvent();

        while(true)
        {
            yield return new WaitForSeconds(_TickDuration);
            TickEvent.Invoke();
        }
    }
}