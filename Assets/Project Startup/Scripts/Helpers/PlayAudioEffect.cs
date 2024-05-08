using UnityEngine;
using NaughtyAttributes;
using ProjectStartup.ScriptableObjects.Helpers;

namespace ProjectStartup.Helpers
{
    public class PlayAudioEffect : MonoBehaviour
    {
        [field: SerializeField, BoxGroup("Audio Properties"), Label("Type Of Audio To Play"), Expandable] private Audio_Type audiotype;
        [field: SerializeField, InfoBox("Plays The Specific Audio Clip(s) Of The Audio Type", EInfoBoxType.Normal), BoxGroup("Information"), Label("Extra Information"), ResizableTextArea] private string info;

        public void OnPlayAudioEffect() { if (audiotype != null) audiotype.PlayAudioEffect(); }
    }
}
