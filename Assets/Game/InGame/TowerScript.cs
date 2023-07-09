using System.Collections;
using System.Collections.Generic;
using Game.InGame;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    private static readonly int RemoveFactor = Shader.PropertyToID("_RemoveFactor");
    public int id;
    public float range = 5f;

    public bool canShoot = true;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(SwitchToTower());
    }

    // Update is called once per frame
    private void Update()
    {
        if (canShoot)
            Shoot();
    }

    private void Shoot()
    {
        var closestUnits = UnitManager.Instance.GetUnitsInRange(transform.position, range);
        closestUnits = closestUnits.FindAll(unit => unit.id == id);
        var closestUnit = GetClosestFromList(closestUnits);
        if (closestUnit != null)
        {
            canShoot = false;
            if (closestUnit.id != id)
            {
                StartCoroutine(closestUnit.Remove());
                StartCoroutine(Remove());
            }
        }
    }

    private UnitScript GetClosestFromList(List<UnitScript> units)
    {
        UnitScript closestUnit = null;
        var closestDistance = float.MaxValue;
        foreach (var unit in units)
        {
            var distance = Vector3.Distance(transform.position, unit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestUnit = unit;
            }
        }

        return closestUnit;
    }

    public IEnumerator UpdateVisuals()
    {
        _spriteRenderer.sprite = TowerManager.Instance.towerSprites[id];
        float removeTime = 1;
        while (removeTime > 0)
        {
            removeTime -= 0.1f;
            _spriteRenderer.material.SetFloat(RemoveFactor, removeTime);
            yield return new WaitForSeconds(0.01f);
        }

        canShoot = true;
        yield return null;
    }

    private IEnumerator SwitchToTower()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(5f);
            id += 1;
            id %= 5;
            StartCoroutine(UpdateVisuals());
        }
    }

    public IEnumerator Remove()
    {
        float removeTime = 0;
        while (removeTime < 1)
        {
            removeTime += 0.1f;
            _spriteRenderer.material.SetFloat(RemoveFactor, removeTime);
            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }
}