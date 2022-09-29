using UnityEngine;
using System;

namespace WebGLAudioStream
{

public class AudioBufferSender : MonoBehaviour
{
    public AudioData audioData;
    public float initDelay = 0.5f;
    public int chunkSize = 2048;
    float[] _buf = null;
    int _clipPos = 0;
    int _bufPos = 0;

    public void Init()
    {
        if (audioData == null || !audioData.clip) return;
        _buf = new float[chunkSize];
        Lib.Init(audioData.clip.frequency, initDelay);
    }

    public void SetVolume(float volume)
    {
        Lib.SetVolume(volume);
    }

    void Update()
    {
        if (audioData == null) return;

        var clip = audioData.clip;
        if (clip == null) return;

        if (_buf == null) return;

        int n = (int)(clip.frequency * Time.unscaledDeltaTime);
        if (n > clip.samples) return;
        if (_clipPos + n >= clip.samples) n = clip.samples - _clipPos;
        var data = new float[n];

        System.Array.Copy(audioData.data, _clipPos, data, 0, n);
        _clipPos += n;
        if (_clipPos >= clip.samples) _clipPos = 0;

        var len = n;
        if (_bufPos + len >= chunkSize) len = chunkSize - _bufPos;
        Array.Copy(data, 0, _buf, _bufPos, len);
        _bufPos += len;

        if (_bufPos >= chunkSize)
        {
            Lib.Play(_buf, chunkSize);
            Array.Clear(_buf, 0, chunkSize);
            _bufPos = 0;

            var rest = n - len;
            if (rest > 0)
            {
                Array.Copy(data, len - 1, _buf, 0, rest);
                _bufPos += rest;
            }
        }
    }
}

}