using System.Collections.Generic;
using UnityEngine;

namespace Game.InGame
{
    public class PathManager : MonoBehaviour
    {
        public static PathManager Instance;
        public List<GameObject> pathNodes;
        public float totalDistance;


        // Start is called before the first frame update
        private void Awake()
        {
            Instance = this;
            pathNodes = new List<GameObject>();
            foreach (Transform child in transform) pathNodes.Add(child.gameObject);
            for (var i = 0; i < pathNodes.Count - 1; i++)
            {
                var node1 = pathNodes[i].transform;
                var node2 = pathNodes[i + 1].transform;
                // Debug.DrawLine(node1.position, node2.position, Color.red, 10f);
                totalDistance += Vector3.Distance(node1.position, node2.position);
            }
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public Vector2 GetPositionOnPath(float distance)
        {
            var currentDistance = 0f;
            for (var i = 0; i < pathNodes.Count - 1; i++)
            {
                var node1 = pathNodes[i].transform;
                var node2 = pathNodes[i + 1].transform;
                var d = Vector3.Distance(node1.position, node2.position);
                if (currentDistance + d > distance)
                {
                    var t = (distance - currentDistance) / d;
                    return Vector3.Lerp(node1.position, node2.position, t);
                }

                currentDistance += d;
            }

            return Vector2.zero;
        }
    }
}