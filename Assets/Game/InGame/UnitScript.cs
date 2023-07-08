using UnityEngine;

namespace Game.InGame
{
    public class UnitScript : MonoBehaviour
    {
        public float positionOnPath;
        public float speed = 1;

        private float _t;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            _t += Time.deltaTime * 0.1f * speed;
            _t %= 1;
            var posOnPath =
                PathManager.Instance.GetPositionOnPath(Mathf.Lerp(0, PathManager.Instance.totalDistance, _t));
            transform.position = posOnPath;
        }
    }
}