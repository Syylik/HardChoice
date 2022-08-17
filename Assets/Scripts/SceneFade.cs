using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    private static int sceneToLoad;
    private static Animator anim;
    private void Awake() => anim = GetComponent<Animator>();
    public static void ChangeScene(int loadScene)
    {
        anim.SetTrigger("fade");
        sceneToLoad = loadScene;
    }
    public void LoadScene() => SceneManager.LoadScene(sceneToLoad);
    public static void PlayAnim() => anim.SetTrigger("transition");
}
