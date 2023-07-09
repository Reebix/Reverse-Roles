using System.Collections;
using SharpUI.Source.Common.UI.Elements.Button;
using SharpUI.Source.Common.Util.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.MainMenu
{
    public class MainMenuScript : MonoBehaviour
    {
        private static readonly int RemoveFactor = Shader.PropertyToID("_RemoveFactor");
        [SerializeField] private RectButton playButton;
        [SerializeField] private RectButton toMetaProgressionButton;
        [SerializeField] private RectButton toMainMenuButton;
        [SerializeField] private RectButton notImplementedButton;

        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject metaProgressionPanel;

        [SerializeField] private Image worm, tower;
        private int _currentPanelOffset;

        private int _wantedPanelOffset;
        private Vector3 _wormPos;

        private void Start()
        {
            _wormPos = worm.transform.position;
            playButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnPlayButtonClicked);
            toMetaProgressionButton.GetEventListener().ObserveOnClicked()
                .SubscribeWith(this, OnToMetaProgressionButtonClicked);
            toMainMenuButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnToMainMenuButtonClicked);
            notImplementedButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnToMainMenuButtonClicked);
            EventSystem.current.SetSelectedGameObject(playButton.gameObject);
            StartCoroutine(TutorialRoutine());
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                EventSystem.current.SetSelectedGameObject(playButton.gameObject);
            if (_currentPanelOffset != _wantedPanelOffset)
            {
                _currentPanelOffset = (int)Mathf.Lerp(_currentPanelOffset, _wantedPanelOffset, Time.deltaTime * 10);
                if (Mathf.Abs(_currentPanelOffset - _wantedPanelOffset) <= 10) _currentPanelOffset = _wantedPanelOffset;

                mainMenuPanel.transform.localPosition = new Vector3(_currentPanelOffset, 0, 0);
                metaProgressionPanel.transform.localPosition = new Vector3(_currentPanelOffset + 1920, 0, 0);
            }
        }

        private IEnumerator TutorialRoutine()
        {
            var wormPos = _wormPos;
            var speed = 200f;
            var targetPos = wormPos + new Vector3(speed, 0f, 0f);


            tower.material.SetFloat(RemoveFactor, 0f);
            worm.material.SetFloat(RemoveFactor, 0f);

            var startTime = Time.time;
            var journeyLength = Vector3.Distance(wormPos, targetPos);

            while (Time.time < startTime + 4f)
            {
                var distanceCovered = (Time.time - startTime) * speed;
                var fractionOfJourney = distanceCovered / journeyLength;
                worm.transform.position = Vector3.Lerp(wormPos, targetPos, fractionOfJourney);

                if (Time.time < startTime + 2f)
                {
                    tower.material.SetFloat(RemoveFactor, Mathf.Lerp(0f, 1f, (fractionOfJourney - 0.5f) * 2));
                    worm.material.SetFloat(RemoveFactor, Mathf.Lerp(0f, 1f, (fractionOfJourney - 0.5f) * 2));
                }

                yield return null;
            }

            if (gameObject.activeSelf)
                StartCoroutine(TutorialRoutine());
        }

        private void OnToMainMenuButtonClicked(Unit obj)
        {
            _wantedPanelOffset = 0;
            EventSystem.current.SetSelectedGameObject(toMetaProgressionButton.gameObject);
        }

        private void OnToMetaProgressionButtonClicked(Unit obj)
        {
            _wantedPanelOffset = -1920;
            EventSystem.current.SetSelectedGameObject(toMainMenuButton.gameObject);
        }

        private void OnPlayButtonClicked(Unit obj)
        {
            FadeManager.Instance.FadeIn();
            StartCoroutine(GoToGameScene());
        }

        private IEnumerator GoToGameScene()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("Game/InGame/MainScene");
        }
    }
}