using UnityEngine;

public class LaggyProcess : MonoBehaviour
{
    void Update()
    {
        for(int I = 0; I <= 1000; I++)
        {
            Debug.Log("Fishy...");
        }
    }
}
