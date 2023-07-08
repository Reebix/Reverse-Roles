using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameMaster : MonoBehaviour
    {
        public static int Difficulty = 1;

        [SerializeField] private int _difficulty = 1;

        public Slider difficultySlider;
        private bool _isDifficultySliderNotNull;

        // Start is called before the first frame update
        private void Start()
        {
            _difficulty = Difficulty;
            _isDifficultySliderNotNull = difficultySlider != null;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isDifficultySliderNotNull) _difficulty = (int)difficultySlider.value;
            Difficulty = _difficulty;
        }
    }
}