using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Core Systems")]
    [SerializeField] private GameObject coreSystemsObject;

    // ---

    private void Start()
    {
        if (coreSystemsObject != null)
        {
            GameObject core = Instantiate(coreSystemsObject);

            DontDestroyOnLoad(core);
        }

        SaveSystem.LoadSettings();
        SaveSystem.LoadProgress();

        SceneLoader.Instance.LoadScene(SceneIds.MAIN_MENU_SCENE);
    }

}
