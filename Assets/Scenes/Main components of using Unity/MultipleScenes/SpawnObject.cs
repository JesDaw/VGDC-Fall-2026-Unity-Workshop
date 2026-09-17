using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject ObjectToSpawn;
    void Start() => Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
}
