using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public event Action<Scene> OnSceneLoaded;
    public bool IsLoading { get; private set; }

    // ---

    public void LoadScene(string sceneName)
    {
        if (IsLoading) return;

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        IsLoading = true;

        // 1. (Optional) Tell UI Manager to show a fade-out/loading screen here
        // UiManager.Instance.ShowLoadingScreen();

        // 2. Start loading the scene in the background
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Prevent the scene from activating instantly if you want a smooth fade
        operation.allowSceneActivation = false;


        // 3. Wait for the scene to finish loading
        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        // Small buffer for visual smoothness
        yield return new WaitForSeconds(0.2f);

        // 4. Allow the scene to activate after the fade-in
        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            yield return null;
        }

        IsLoading = false;

        OnSceneLoaded?.Invoke(SceneManager.GetActiveScene());
    }
}
