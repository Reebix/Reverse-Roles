using SharpUI.Source.Common.UI.Elements.Button;
using SharpUI.Source.Common.Util.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Escape_Menu
{
    public class InfoPrefabScript : MonoBehaviour
    {
        public RectButton okButton;
        private GameObject _lastSelectedGameObject;

        private void Start()
        {
            okButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnCloseButtonClicked);
        }

        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != okButton.gameObject)
            {
                _lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
                EventSystem.current.SetSelectedGameObject(okButton.gameObject);
            }
        }

        private void OnCloseButtonClicked(Unit obj)
        {
            EventSystem.current.SetSelectedGameObject(_lastSelectedGameObject);
            gameObject.SetActive(false);
        }
    }
}