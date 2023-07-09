using SharpUI.Source.Common.UI.Elements.Button;
using SharpUI.Source.Common.Util.Extensions;
using UniRx;
using UnityEngine;

namespace Game.InGame
{
    public class UnitButtonScript : MonoBehaviour
    {
        public int id;
        private IconButton _button;

        private void Start()
        {
            _button = GetComponent<IconButton>();
            _button.GetEventListener().ObserveOnSelected().SubscribeWith(this, OnUnitSelected);
        }

        private void OnUnitSelected(Unit obj)
        {
            UnitManager.Instance.currentUnitId = id;
        }

        public void Spawn()
        {
            UnitManager.Instance.AddEnergy(-3);
            var unit = Instantiate(UnitManager.Instance.unitPrefab, transform.position, Quaternion.identity);
            unit.GetComponent<UnitScript>().id = id;
        }
    }
}