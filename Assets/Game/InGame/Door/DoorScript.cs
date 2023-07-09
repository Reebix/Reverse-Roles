using System.Collections.Generic;
using SharpUI.Source.Common.UI.Elements.Button;
using SharpUI.Source.Common.Util.Extensions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.InGame.Door
{
    public class DoorScript : MonoBehaviour
    {
        public static DoorScript Instance;
        private static readonly int DissolveFactor = Shader.PropertyToID("_DissolveFactor");
        private static readonly int WhiteFactor = Shader.PropertyToID("_WhiteFactor");
        public Image fillImage;
        public GameObject text, finishPanel;
        public List<Sprite> sprites;
        public int maxHealth = 10;
        public int health = 10;
        public RectButton toMainMenuButton;

        public TextMeshProUGUI endText;
        public int state;
        private SpriteRenderer _spriteRenderer;


        // Start is called before the first frame update
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Instance = this;

            health = maxHealth;
            Damage(0);

            toMainMenuButton.GetEventListener().ObserveOnClicked().SubscribeWith(Instance, OnMainMenuButtonClicked);

            finishPanel.SetActive(false);
        }


        // Update is called once per frame
        private void Update()
        {
        }

        private void OnMainMenuButtonClicked(Unit obj)
        {
            Application.Quit();
        }


        private void OnTestButtonClicked(UnitScript obj)
        {
            Damage(1);
        }

        public void ChangeState(int newState)
        {
            state = newState;
            if (state > 1)
                GameMaster.GameOver();
            else
                _spriteRenderer.sprite = sprites[state];
        }

        public void Damage(int amount)
        {
            health -= amount;
            _spriteRenderer.material.SetFloat(DissolveFactor, 1 - health / (float)maxHealth);
            fillImage.fillAmount = (float)health / maxHealth;
            text.GetComponent<TextMeshProUGUI>().text = health + " / " + maxHealth;
            if (health == 0)

                ChangeState(2);

            else if (health <= maxHealth / 2) ChangeState(1);
        }
    }
}