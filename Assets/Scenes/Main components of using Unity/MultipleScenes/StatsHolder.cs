using UnityEngine;

public class StatsHolder : MonoBehaviour 
{ 
    public float moveSpeed; 
    [SerializeField] GameObject characterPrefab; 
    [SerializeField] StatsSO stats; 

    void Awake() 
    { 
        if (stats != null) moveSpeed = stats.MoveSpeed; 

        if (characterPrefab != null) 
        {
            if (characterPrefab.TryGetComponent<StatsHolder>(out StatsHolder prefabStats)) 
                prefabStats.moveSpeed = moveSpeed; 
        }
    } 
}
