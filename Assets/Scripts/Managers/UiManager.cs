using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiManager : Singleton<UiManager>
{
    [SerializeField] private PanelRenderer panelRenderer;

    private VisualElement root;
    private int panelVersion;

    // Menus
    public MainMenu MainMenu { get; private set; }
    public PauseMenu PauseMenu { get; private set; }
    public SettingsMenu SettingsMenu { get; private set; }
    public Hud Hud { get; private set; }
    public GameUi GameUi { get; private set; }
    public LoadingScreen LoadingScreen { get; private set; }

    #region Unity Lifecycle

    private void Start()
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.OnSceneLoaded += OnSceneLoaded;

        // 💡 1. Process the active scene on boot!
        SetupUiForScene(SceneManager.GetActiveScene());
    }

    private void OnEnable()
    {
        if (panelRenderer != null)
            panelRenderer.RegisterUIReloadCallback(OnUiReload);
    }

    private void OnDisable()
    {
        if (panelRenderer != null)
            panelRenderer.UnregisterUIReloadCallback(OnUiReload);

        if (SceneLoader.Instance != null)
            SceneLoader.Instance.OnSceneLoaded -= OnSceneLoaded;
    }

    #endregion

    #region PanelRenderer Callbacks

    private void OnUiReload(PanelRenderer panelRenderer, VisualElement root, int version)
    {
        this.root = root;
        panelVersion = version;

        // 💡 2. Re-bind and set up menus whenever the PanelRenderer tree reloads!
        SetupUiForScene(SceneManager.GetActiveScene());
    }

    #endregion

    #region Helpers

    public VisualElement GetElement(string name)
    {
        if (root == null)
        {
            Debug.LogWarning($"UiManager: Tried to query '{name}' before visual tree root was set!");
            return null;
        }

        VisualElement elem = root.Q<VisualElement>(name);
        if (elem == null)
        {
            Debug.LogError($"UiManager: Could not find element named '{name}' in UXML!");
        }

        return elem;
    }

    public void CloseAllMenus()
    {
        Debug.Log("Closing all menus");

        MainMenu?.Close();
        PauseMenu?.Close();
        SettingsMenu?.Close();
        GameUi?.Close();
        LoadingScreen?.Close();
        Hud?.Close();
    }

    #endregion

    #region Scene Handling

    private void OnSceneLoaded(Scene scene)
    {
        SetupUiForScene(scene);
    }

    private void SetupUiForScene(Scene scene)
    {
        // If root isn't bound yet, OnUiReload will trigger this again once it's ready!
        if (root == null) return;

        Debug.Log($"Setting up UI for scene: {scene.name}");

        // 1. Instantiate ALL menu wrappers so every root element gets bound
        MainMenu = new MainMenu(this);
        PauseMenu = new PauseMenu(this);
        SettingsMenu = new SettingsMenu(this);
        Hud = new Hud(this);
        GameUi = new GameUi(this);
        LoadingScreen = new LoadingScreen(this);

        // 2. Hide every menu across the board
        CloseAllMenus();

        // 3. Open ONLY the menus intended for the active scene
        if (scene.name == SceneIds.MENU_SCENE)
        {
            MainMenu.Open();
        }
        else
        {
            Hud.Open();
            GameUi.Open();
        }
    }

    #endregion
}
