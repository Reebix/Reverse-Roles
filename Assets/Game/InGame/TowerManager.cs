using System.Collections.Generic;
using Game;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    public List<Sprite> towerSprites;
    private readonly List<TowerScript> _towers = new();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _towers.AddRange(FindObjectsOfType<TowerScript>());
        for (var i = _towers.Count - 1; i >= 0; i--)
            if (GameMaster.Difficulty < i + 1)
                _towers[i].gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}