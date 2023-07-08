using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Game
{
    public class FadeManager : MonoBehaviour
    {
        public int fadeDirection = -1;
        public static FadeManager Instance;


        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (gameObject.activeSelf)
            {
                transform.localScale += Vector3.one * (Time.deltaTime * 2 * fadeDirection);
                if (fadeDirection == 1 && transform.localScale.x > 1f)
                {
                    transform.localScale = Vector3.one;
                }

                if (fadeDirection == -1 && transform.localScale.x <= 0)
                {
                    gameObject.SetActive(false);
                    transform.localScale = Vector3.zero;
                }
            }
        }

        public void FadeOut()
        {
            gameObject.SetActive(true);
            fadeDirection = -1;
        }

        public void FadeIn()
        {
            gameObject.SetActive(true);
            fadeDirection = 1;
        }
    }
}