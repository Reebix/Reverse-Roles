using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using SharpUI.Source.Common.UI.Elements.Button;
using SharpUI.Source.Common.Util.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public InputActionReference escapeAction;
    public RectButton resumeButton, quitButton, mainMenuButton, settingsButton;

    // Start is called before the first frame update
    private void Start()
    {
        escapeAction.action.Enable();
        resumeButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnResumeButtonClicked);
        quitButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnQuitButtonClicked);
        mainMenuButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnMainMenuButtonClicked);
        settingsButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnSettingsButtonClicked);
    }

    private void OnSettingsButtonClicked(Unit obj)
    {
        throw new NotImplementedException();
    }

    private void OnMainMenuButtonClicked(Unit obj)
    {
        transform.localScale = Vector3.zero;
        FadeManager.Instance.FadeIn();
        StartCoroutine(GoToMainMenu());
    }
    
    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game/MainMenu/MainMenu");
    }

    private void OnQuitButtonClicked(Unit obj)
    {
        Application.Quit();
    }

    private void OnResumeButtonClicked(Unit obj)
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        ToggleEscapeMenu();
    }

    private void ToggleEscapeMenu()
    {
        if (escapeAction.action.triggered)
        {
            if (transform.localScale == Vector3.one * 2)
                transform.localScale = Vector3.zero;
            else
            {
                transform.localScale = Vector3.one * 2;
                EventSystem.current.SetSelectedGameObject(settingsButton.gameObject);
            }
                
        }
    }
}