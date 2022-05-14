using UnityEngine;

public class MainMenu : MonoBehaviour
{
    TransitionManager _transitionManager;
    bool credits = false;
    [SerializeField] GameObject credits_obj;

    private void Start()
    {
        _transitionManager = FindObjectOfType<TransitionManager>();
        credits_obj.SetActive(credits);
    }

    private void Update()
    {
        credits_obj.SetActive(credits);
    }

    public void btn_Quit() => Application.Quit();
    public void btn_Start()
    {
        Debug.Log("fire");
        StartCoroutine(_transitionManager.Transition(2));
    }

    public void toggle_credits() => credits = !credits;
}
