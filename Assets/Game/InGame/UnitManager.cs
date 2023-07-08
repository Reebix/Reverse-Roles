using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.InGame
{
    public class UnitManager : MonoBehaviour
    {
        public GameObject unitPrefab;
        public InputActionReference spawnUnitButton;
        public float spawnDelay = 0.1f;

        // Start is called before the first frame update
        private void Start()
        {
            spawnUnitButton.action.Enable();
            spawnUnitButton.action.performed += _ => StartCoroutine(SpawnUnits(20));
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void SpawnUnit()
        {
            var unit = Instantiate(unitPrefab, transform.position, Quaternion.identity);
            unit.transform.SetParent(transform);
        }
        
        private IEnumerator SpawnUnits(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                SpawnUnit();
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}