using UnityEngine;

namespace JAGD.Kit.Helpers
{
    [CreateAssetMenu(fileName = "NewPlayerAudioData", menuName = "Game Data/Player Audio Data")]
    public class PlayerAudioData : ScriptableObject
    {
        public AudioClip footstepSound;
        public AudioClip jumpSound;
        public AudioClip landSound;
    }
}
