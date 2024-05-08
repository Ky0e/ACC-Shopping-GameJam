using UnityEngine;
using System.Collections;

namespace ProjectStartup.Helpers
{
    /// <summary>
    /// Disables The GameObject With An Audio Source Attached After The Current Clip Has Played
    /// </summary>
    public class DisableAudioEffect : MonoBehaviour
    {
        private IEnumerator DisableOnEnable()
        {
            while (GetComponent<AudioSource>().isPlaying) { yield return null; }
            gameObject.SetActive(false);
        }

        public void OnAudioEnabled() => StartCoroutine(DisableOnEnable());
    }
}
