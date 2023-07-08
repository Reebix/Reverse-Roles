using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game
{
    public class GameMaster : MonoBehaviour
    {
        public static int Difficulty = 1;
        public static InputActionAsset InputActionAsset;
        public InputActionAsset inputActionAsset;
        [SerializeField] private int difficulty = 1;

        public Slider difficultySlider;
        private bool _isDifficultySliderNotNull;

        // Start is called before the first frame update
        private void Start()
        {
            InputActionAsset = inputActionAsset;
            difficulty = Difficulty;
            _isDifficultySliderNotNull = difficultySlider != null;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isDifficultySliderNotNull) difficulty = (int)difficultySlider.value;
            Difficulty = difficulty;
        }
    }
}