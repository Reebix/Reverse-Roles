using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject fadeCircle;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (fadeCircle.activeSelf)
        {
            fadeCircle.transform.localScale -= Vector3.one * (Time.deltaTime * 2);
            if (fadeCircle.transform.localScale.x <= 0) fadeCircle.SetActive(false);
        }
    }
}