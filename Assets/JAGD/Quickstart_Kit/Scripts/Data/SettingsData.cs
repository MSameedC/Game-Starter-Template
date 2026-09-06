namespace JAGD.Kit.Data
{
    [System.Serializable]
    public static class SettingsData
    {
        private const float defaultVolume = .8f;
        public static float MasterVolume = defaultVolume;
        public static float MusicVolume = defaultVolume;
        public static float AmbientVolume = defaultVolume;
        public static float SfxVolume = defaultVolume;
    }
}
