using UnityEngine;

public static class SaveSystem
{
    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", SettingsData.MasterVolume);
        PlayerPrefs.SetFloat("MusicVolume", SettingsData.MusicVolume);
        PlayerPrefs.SetFloat("AmbientVolume", SettingsData.AmbientVolume);
        PlayerPrefs.SetFloat("SfxVolume", SettingsData.SfxVolume);
        PlayerPrefs.Save();
    }

    public static void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MasterVolume")) SettingsData.MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        if (PlayerPrefs.HasKey("MusicVolume")) SettingsData.MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("AmbientVolume")) SettingsData.AmbientVolume = PlayerPrefs.GetFloat("AmbientVolume");
        if (PlayerPrefs.HasKey("SfxVolume")) SettingsData.SfxVolume = PlayerPrefs.GetFloat("SfxVolume");
    }

    public static void SaveProgress()
    {
        PlayerPrefs.SetInt("currentLevel", PlayerData.currentLevel);
        PlayerPrefs.SetInt("currency", PlayerData.currency);
        PlayerPrefs.SetString("lastCheckpointId", PlayerData.lastCheckpointId);
        PlayerPrefs.Save();
    }

    public static void LoadProgress()
    {
        if (PlayerPrefs.HasKey("currentLevel")) PlayerData.currentLevel = PlayerPrefs.GetInt("currentLevel");
        if (PlayerPrefs.HasKey("currency")) PlayerData.currency = PlayerPrefs.GetInt("currency");
        if (PlayerPrefs.HasKey("lastCheckpointId")) PlayerData.lastCheckpointId = PlayerPrefs.GetString("lastCheckpointId");
    }
}
