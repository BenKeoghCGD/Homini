using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    AsyncOperation sceneOperation;
    TransitionManager _transitionManager;
    [SerializeField] VideoPlayer splash;

    void Start()
    {
        _transitionManager = GameObject.FindObjectOfType<TransitionManager>();
        StartCoroutine(beginAnim());
    }

    private IEnumerator beginAnim()
    {
        splash.Prepare();
        yield return _transitionManager.fadeOut();
        splash.Play();
        yield return new WaitForSecondsRealtime((float)splash.length * 1.33f);
        yield return _transitionManager.transition(1);
    }
}
