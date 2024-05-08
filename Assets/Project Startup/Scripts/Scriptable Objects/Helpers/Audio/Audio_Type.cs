using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Audio;
using ProjectStartup.Helpers;

namespace ProjectStartup.ScriptableObjects.Helpers
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Helper Objects/Audio/Audio Type", fileName = "New Audio Type")]
    public class Audio_Type : ScriptableObject
    {
        [field: SerializeField, BoxGroup("Audio Properties"), Label("Audio Random Container")] private AudioResource clipSet;
        [field: SerializeField, BoxGroup("Audio Properties"), Label("Audio Mixer Group")] private AudioMixerGroup mixerGroup;
        [field: SerializeField, Expandable, BoxGroup("Object Pooling Properties"), Label("Object Pool")] private ObjectPool pool;
        [field: SerializeField, InfoBox("A Collection Of Similiar Audio Clip(s) To Be Played At Runtime", EInfoBoxType.Normal), BoxGroup("Information"), Label("Extra Information"), ResizableTextArea] private string info;

        /// <summary>
        /// Plays The Specified Audio Clip Set To The Assigned Mixer Group ( If Assigned )
        /// </summary>
        public void PlayAudioEffect() => PlayEffect();

        /// <summary>
        /// Plays At A Specific Position, The Specified Audio Clip Set To The Assigned Mixer Group ( If Assigned )
        /// </summary>
        public void PlayAudioEffect(Transform _parent)
        {
            GameObject _newSourceOBJ = PlayEffect();
            if (_newSourceOBJ != null) { _newSourceOBJ.SetActive(true); }
            _newSourceOBJ.transform.parent = _parent;
        }

        /// <summary>
        /// Plays At A Specific Position, The Specified Audio Clip Set To The Assigned Mixer Group ( If Assigned )
        /// </summary>
        public void PlayAudioEffect(Vector3 _position)
        {
            GameObject _newSourceOBJ = PlayEffect();
            if (_newSourceOBJ != null) { _newSourceOBJ.SetActive(true); }
            _newSourceOBJ.transform.position = _position;
        }

        private GameObject PlayEffect()
        {
            GameObject _newSourceOBJ = pool.GetPooledObject();
            if (_newSourceOBJ != null) { _newSourceOBJ.SetActive(true); }
            AudioSource _source = _newSourceOBJ.GetComponent<AudioSource>();
            if (mixerGroup != null) { _source.outputAudioMixerGroup = mixerGroup; }
            _source.resource = clipSet;
            _source.Play();
            _newSourceOBJ.GetComponent<DisableAudioEffect>().OnAudioEnabled();
            return _newSourceOBJ;
        }
    }
}