using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    TransitionManager _transitionManager;
    [SerializeField] VideoPlayer splash;

    void Start()
    {
        _transitionManager = GameObject.FindObjectOfType<TransitionManager>();
        StartCoroutine(BeginAnim());
    }

    private IEnumerator BeginAnim()
    {
        splash.Prepare();
        yield return _transitionManager.FadeOut();
        splash.Play();
        yield return new WaitForSecondsRealtime((float)splash.length * 1.33f);
        yield return _transitionManager.Transition(1);
    }
}
