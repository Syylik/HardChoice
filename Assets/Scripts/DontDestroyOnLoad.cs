using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(GameObject.FindObjectOfType<DontDestroyOnLoad>() != this) Destroy(gameObject);
    }
}
