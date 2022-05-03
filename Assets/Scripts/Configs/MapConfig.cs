using UnityEngine;

[CreateAssetMenu(menuName = "Map Configuration")]
public class MapConfig : ScriptableObject
{
    
    /*
        just let me scream
        ...
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒        ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒                    ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒                            ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒        ░░░░░░░░░░░░░░░░        ▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒      ░░░░░░░░░░░░░░░░░░░░░░░░      ▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒      ░░░░░░░░██░░░░░░██░░░░░░░░░░░░    ▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒      ░░░░░░░░██░░░░░░░░░░██░░░░░░░░░░░░    ▒▒▒▒▒▒▒▒
▒▒▒▒▒▒      ░░░░▒▒▓▓░░░░░░░░░░░░░░░░░░▒▒▓▓░░░░░░░░    ▒▒▒▒▒▒
▒▒▒▒▒▒    ░░░░▒▒████▓▓░░░░░░░░░░░░░░▒▒████▒▒░░░░░░░░    ▒▒▒▒
▒▒▒▒      ░░▓▓██  ████░░░░░░░░░░░░░░████  ██▓▓░░░░░░░░  ▒▒▒▒
▒▒      ░░▒▒  ██████░░░░░░      ░░░░░░██████  ▓▓░░░░░░░░  ▒▒
▒▒      ░░▓▓  ████░░░░  ██████████  ░░░░████  ▒▒░░░░░░░░  ▒▒
▒▒    ░░░░░░▒▒  ░░░░  ████▓▓██▒▒████  ░░░░  ▓▓░░░░░░░░░░    
      ░░░░░░░░░░░░  ▓▓██  ██▒▒██  ██▓▓  ░░░░░░░░░░░░░░░░░░  
      ░░░░░░░░░░  ████▓▓██  ██  ██▓▓████  ░░░░░░░░░░░░░░░░  
      ░░░░░░░░░░  ▓▓▓▓██▓▓██████▓▓██▓▓▓▓  ░░░░░░░░░░░░░░░░  
    ░░░░░░▓▓▒▒▒▒██████▓▓██▓▓██▓▓██▓▓██████▓▓▓▓▓▓░░░░░░░░░░░░
    ░░▓▓▒▒░░░░░░  ▓▓▓▓██▓▓▓▓██▓▓▒▒██▓▓▒▒  ░░░░░░▒▒▓▓░░░░░░░░
    ▒▒░░░░░░▓▓▒▒▓▓████▓▓▓▓  ▒▒  ▓▓▓▓████▒▒▒▒▓▓░░░░░░▒▒░░░░░░
  ▓▓░░░░▒▒▒▒░░░░▒▒▒▒      ▒▒▒▒▒▒      ▒▒▒▒░░░░▒▒▓▓░░░░▒▒░░░░
    ░░▒▒░░░░░░▓▓░░▒▒██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▒▒▒░░░░░░▒▒░░░░░░░░
    ░░░░░░▒▒▒▒░░░░░░▒▒██▒▒▒▒▒▒▒▒▒▒▒▒████▒▒░░▒▒░░░░░░▓▓░░░░░░
  ░░░░░░▓▓░░░░░░▒▒░░▒▒██▒▒▒▒▒▒▒▒▒▒▒▒██▒▒░░░░░░▓▓░░░░░░░░░░░░
  ░░░░░░░░░░░░░░░░▒▒░░▒▒██▒▒▒▒▒▒▒▒██▒▒▒▒░░░░░░░░▒▒░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░▒▒▒▒████▒▒▒▒████▒▒░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░▓▓▓▓████████▓▓▒▒░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░▒▒▓▓████▒▒▒▒░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░▓▓▒▒▓▓▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
     * 
     */
    [SerializeField] private int maxSectors = 5;
    [SerializeField] [Range(0, 0.2F)] private float padding = 0.2F;
    [SerializeField] private GameObject islandPrefab;
    
    private float wrapperWidth = 1;

    public int MaxSectors
    {
        get => maxSectors;
        set => maxSectors = value;
    }

    public float WrapperWidth
    {
        get => wrapperWidth;
        set => wrapperWidth = value;
    }

    public float Padding
    {
        get => padding;
        set => padding = value;
    }

    public GameObject IslandPrefab
    {
        get => islandPrefab;
        set => islandPrefab = value;
    }

    public float SectorDistance
    {
        get => sectorDistance;
        set => sectorDistance = value;
    }

    private float sectorDistance = 1;
}