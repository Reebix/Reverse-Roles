using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.InGame.Door
{
    public class DoorScript : MonoBehaviour
    {
        public static DoorScript Instance;
        public Image FillImage;
        public GameObject Text;
        public List<Sprite> sprites;
        public int maxHealth = 10;
        public int health = 10;

        public int state;


        // Start is called before the first frame update
        private void Start()
        {
            Instance = this;
            health = maxHealth;
            Damage(0);
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTestButtonClicked(UnitScript obj)
        {
            Damage(1);
        }

        public void ChangeState(int newState)
        {
            state = newState;
            if (state > 1) gameObject.SetActive(false);
            else GetComponent<SpriteRenderer>().sprite = sprites[state];
        }

        public void Damage(int amount)
        {
            health -= amount;
            FillImage.fillAmount = (float)health / maxHealth;
            Text.GetComponent<TextMeshProUGUI>().text = health < 0 ? "0" : health + " / " + maxHealth;
            if (health <= 0)
            {
                health = 0;
                ChangeState(2);
            }
            else if (health <= maxHealth / 2)
            {
                ChangeState(1);
            }
        }
    }
}