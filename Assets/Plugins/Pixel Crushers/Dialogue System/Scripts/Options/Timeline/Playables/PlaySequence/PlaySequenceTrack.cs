// Recompile at 8/24/2023 2:09:42 PM
#if USE_TIMELINE
#if UNITY_2017_1_OR_NEWER
// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace PixelCrushers.DialogueSystem
{

    [TrackColor(0.855f, 0.8623f, 0.87f)]
    [TrackClipType(typeof(PlaySequenceClip))]
    [TrackBindingType(typeof(GameObject))]
    public class PlaySequenceTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<PlaySequenceMixerBehaviour>.Create(graph, inputCount);
        }
    }
}
#endif
#endif
