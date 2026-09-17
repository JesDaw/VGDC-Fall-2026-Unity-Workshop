using UnityEngine;

public class DontDestroyOnLoadSceneTransition : MonoBehaviour
{
    public static DontDestroyOnLoadSceneTransition Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
