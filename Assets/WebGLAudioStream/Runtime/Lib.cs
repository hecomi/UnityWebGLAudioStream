using UnityEngine;
using System.Runtime.InteropServices;

namespace WebGLAudioStream
{

static public class Lib
{
#if UNITY_EDITOR
    public static void Init(int sampleRate, float initDelay) { Debug.Log($"Init({sampleRate}, {initDelay})"); }
    public static void Play(float[] array, int size) { Debug.Log($"Play({array}, {size})"); }
    public static void SetVolume(float volume) { Debug.Log($"SetVolume({volume})"); }
#else
    [DllImport("__Internal")]
    public static extern void Init(int sampleRate, float initDelay);
    [DllImport("__Internal")]
    public static extern void Play(float[] array, int size);
    [DllImport("__Internal")]
    public static extern void SetVolume(float volume);
#endif
}

}