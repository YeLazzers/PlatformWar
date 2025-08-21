using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner s_instance;

    public static CoroutineRunner Instance
    {
        get
        {
            if (s_instance == null)
            {
                var go = new GameObject("CoroutineRunner");
                s_instance = go.AddComponent<CoroutineRunner>();
                DontDestroyOnLoad(go);
            }
            return s_instance;
        }
    }
}
