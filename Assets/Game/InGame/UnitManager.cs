using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game.InGame
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;
        public GameObject unitPrefab;
        public InputActionReference spawnUnitButton;
        private int _energy = 12;
        public Image energyBar;
        public int currentUnitId = -1;
        public List<UnitButtonScript> unitButtons = new(5);
        public List<Sprite> unitSprites = new(5);
        public List<UnitScript> units;

        private List<GameObject> _unitButtonsObjects = new(5);
        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(RegenerateEnergy());
            spawnUnitButton.action.Enable();
            spawnUnitButton.action.performed += _ => SpawnUnit();
            unitButtons.ForEach(button => button.id = unitButtons.IndexOf(button));
            unitButtons.ForEach(button => _unitButtonsObjects.Add(button.gameObject));
        }

        // Update is called once per frame
        private void Update()
        {

            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(_unitButtonsObjects[currentUnitId]);
            }
        }

        private void SpawnUnit()
        {
            if (currentUnitId == -1) return;
            if (_energy < 3) return;
            var button = unitButtons[currentUnitId];
            button.Spawn();
        }

        public void AddEnergy(int amount)
        {
            _energy += amount;
            if (_energy < 0) _energy = 0;
            else if (_energy > 12) _energy = 12;
            energyBar.fillAmount = (float)_energy / 12;
        }

        private IEnumerator RegenerateEnergy()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(2f);
                AddEnergy(1);
            }
        }


        public List<UnitScript> GetUnitsInRange(Vector3 transformPosition, float range)
        {
            return units.Where(unit => Vector3.Distance(unit.transform.position, transformPosition) < range).ToList();
        }
    }
}