using JAGD.Kit.Helpers;
using JAGD.Kit.Utilities;
using UnityEngine;

namespace JAGD.Kit.Managers
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Core Systems")]
        [SerializeField] private GameObject coreSystemsObject;

        [Header("Setting")]
        [SerializeField] private string startingScene = SceneIds.DEMO_SCENE;

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

            SceneLoader.Instance.LoadScene(startingScene);
        }
    }
}
