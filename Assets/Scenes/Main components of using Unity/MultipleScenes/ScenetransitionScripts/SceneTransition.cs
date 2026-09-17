using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string NextSceneName;
    [SerializeField] int NextSceneindex;
    [SerializeField] bool useIndex;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) NextScene();
    }

    void NextScene()
    {
        if (useIndex) SceneManager.LoadScene(NextSceneindex);
        else SceneManager.LoadScene(NextSceneName);
    }
}
