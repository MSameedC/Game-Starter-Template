using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    // ---

    [SerializeField] private UIDocument uiDoc; // Container for menus

    private VisualElement root;

    // Menus
    public MainMenu MainMenu { get; private set; }
    public PauseMenu PauseMenu { get; private set; }
    public SettingsMenu SettingsMenu { get; private set; }
    public Hud Hud { get; private set; }
    public GameUi GameUi { get; private set; }
    public LoadingScreen LoadingScreen { get; private set; }

    // ---

    private void Awake()
    {
        SetInstance();
        root = uiDoc.rootVisualElement;
        CloseAllMenus();
    }

    private void SetInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Debug.Log("UiManager enabled");
        SceneLoader.Instance.OnSceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        Debug.Log("UiManager disabled");
        SceneLoader.Instance.OnSceneLoaded -= OnSceneLoaded;
    }

    public VisualElement GetElement(string name)
    {
        return root.Q<VisualElement>(name);
    }

    public void CloseAllMenus()
    {
        Debug.Log("Closing all menus");

        if (MainMenu != null) MainMenu.Close();
        if (PauseMenu != null) PauseMenu.Close();
        if (SettingsMenu != null) SettingsMenu.Close();
        if (GameUi != null) GameUi.Close();
        if (LoadingScreen != null) LoadingScreen.Close();
        if (Hud != null) Hud.Close();
    }

    private void OnSceneLoaded(Scene scene)
    {
        Debug.Log($"Ui loaded");

        CloseAllMenus();

        if (scene.name == SceneIds.MAIN_MENU_SCENE)
        {
            MainMenu = new MainMenu(this);
            SettingsMenu = new SettingsMenu(this);
            LoadingScreen = new LoadingScreen(this);

            MainMenu.Open();
        }
        else
        {
            PauseMenu = new PauseMenu(this);
            SettingsMenu = new SettingsMenu(this);
            LoadingScreen = new LoadingScreen(this);
            Hud = new Hud(this);
            GameUi = new GameUi(this);
        }
    }
}
