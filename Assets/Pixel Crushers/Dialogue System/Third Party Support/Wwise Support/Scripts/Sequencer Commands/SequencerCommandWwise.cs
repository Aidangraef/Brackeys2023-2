using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    /// <summary>
    /// Syntax: Wwise(eventName, [gameObject])
    /// 
    /// - eventName: A Wwise standard event name.
    /// - gameObject: GameObject to play event on; defaults to speaker.
    /// </summary>
    public class SequencerCommandWwise : SequencerCommand
    {

        protected uint playingID;
        protected bool isEventPlaying = false;

        protected virtual void Awake()
        {
            var eventName = GetParameter(0);
            var subject = GetSubject(1, speaker);
            var subjectGO = (subject != null) ? subject.gameObject : null;
            if (string.IsNullOrEmpty(eventName))
            {
                if (DialogueDebug.logWarnings) Debug.LogWarning($"Dialogue System: Sequence: Wwise({GetParameters()}): Event name is blank.");
                Stop();
            }
            else
            {
                playingID = AkSoundEngine.PostEvent(eventName, subjectGO, (uint)AkCallbackType.AK_EndOfEvent, AkEventCallback, null);
                if (playingID == AkSoundEngine.AK_INVALID_PLAYING_ID)
                {
                    if (DialogueDebug.logWarnings) Debug.LogWarning($"Dialogue System: Sequence: Wwise({GetParameters()}): Wwise failed to start event '{eventName}'.");
                    Stop();
                }
                else
                {
                    if (DialogueDebug.logInfo) Debug.Log($"Dialogue System: Sequence: Wwise({GetParameters()})");
                    isPlaying = true;
                }
            }
        }

        protected void AkEventCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            if (in_type == AkCallbackType.AK_EndOfEvent && (in_info as AkEventCallbackInfo).playingID == playingID)
            {
                isPlaying = false;
                Stop();
            }
        }

        protected virtual void OnDestroy()
        {
            if (isPlaying)
            {
                isPlaying = false;
                AkSoundEngine.StopPlayingID(playingID);
            }
        }
    }
}
