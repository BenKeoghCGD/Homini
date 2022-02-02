using UnityEngine;

public class MainMenu : MonoBehaviour
{
    TransitionManager _transitionManager;

    private void Start()
    {
        _transitionManager = FindObjectOfType<TransitionManager>();
    }

    public void btn_Quit() { Application.Quit(); }
    public void btn_Start() { /*SECOND MENU UI*/ }
    public void btn_Settings() { /*SETTINGS BUTTON*/ }
    public void btn_Credits() { /*CREDITS SCENE*/ }
}
