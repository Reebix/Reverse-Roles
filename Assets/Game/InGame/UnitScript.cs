using System.Collections;
using Game.InGame.Door;
using UnityEngine;

namespace Game.InGame
{
    public class UnitScript : MonoBehaviour
    {
        private static readonly int RemoveFactor = Shader.PropertyToID("_RemoveFactor");
        public float positionOnPath;
        public float speed = 1;
        public int id;
        private bool _shouldMove = true;
        private SpriteRenderer _spriteRenderer;

        private float _t;

        // Start is called before the first frame update
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = UnitManager.Instance.unitSprites[id];
            UnitManager.Instance.units.Add(this);
        }

        // Update is called once per frame
        private void Update()
        {
            _t += Time.deltaTime * 0.1f * speed * (_shouldMove ? 1 : 0);
            if (_t > 1 && _shouldMove)
            {
                _shouldMove = false;
                StartCoroutine(Remove());
                DoorScript.Instance.Damage(1);
            }

            if (_shouldMove)
                transform.position =
                    PathManager.Instance.GetPositionOnPath(Mathf.Lerp(0, PathManager.Instance.totalDistance, _t));
        }

        public IEnumerator Remove()
        {
            UnitManager.Instance.units.Remove(this);
            float removeTime = 0;
            while (removeTime < 1)
            {
                removeTime += 0.03f;
                _spriteRenderer.material.SetFloat(RemoveFactor, removeTime);
                yield return new WaitForSeconds(0.01f);
            }

            Destroy(gameObject);
            yield return null;
        }
    }
}