using UnityEngine;

namespace WebGLAudioStream
{

[CreateAssetMenu(menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    public AudioClip clip;
    public float[] data;

    [ContextMenu("Decode")]
    public void Decode()
    {
        if (!clip) return;
        data = new float[clip.samples];
        clip.GetData(data, 0);
    }
}

}