using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game
{
    public class GameMaster : MonoBehaviour
    {
        public static int Difficulty = 1;
        public static InputActionAsset InputActionAsset;
        private static float _timer = 310f;
        public InputActionAsset inputActionAsset;
        public GameObject timerText;
        [SerializeField] private int difficulty = 1;
        public Slider difficultySlider;
        private bool _isDifficultySliderNotNull;
        private TextMeshProUGUI _textMeshProUGUI;


        // Start is called before the first frame update
        private void Start()
        {
            if (timerText != null)
                _textMeshProUGUI = timerText.GetComponent<TextMeshProUGUI>();

            InputActionAsset = inputActionAsset;
            difficulty = Difficulty;
            _isDifficultySliderNotNull = difficultySlider != null;
            if (timerText != null)
                InvokeRepeating("CountDownTimer", 1f, 1f);
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isDifficultySliderNotNull) difficulty = (int)difficultySlider.value;
            Difficulty = difficulty;

            if (_timer <= 0f) GameWon();
        }

        private void CountDownTimer()
        {
            _timer -= 1f; // Decrease the timer by 1 second
            _textMeshProUGUI.text = FormatTime(_timer); // Update the text field
        }

        private static string FormatTime(float time)
        {
            var minutes = Mathf.FloorToInt(time / 60f);
            var seconds = Mathf.FloorToInt(time % 60f);

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public static void GameOver()
        {
            Debug.Log("Game Over :) ended on difficulty " + Difficulty + " time left: " + FormatTime(_timer));
        }

        public static void GameWon()
        {
            Debug.Log("Game Won :( managed to defeat on difficulty: " + Difficulty);
        }
    }
}