using UnityEngine;
using UnityEngine.Audio;

namespace Sound
{
    public class MixerLevels : MonoBehaviour
    {
        private const string VolumeName = "Volume";

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Vector2 _volumeRange;
        
        public void Mute() => 
            _mixer.SetFloat(VolumeName, _volumeRange.x);

        public void Unmute() => 
            _mixer.SetFloat(VolumeName, _volumeRange.y);
    }
}
