using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Screen : MonoBehaviour
{
    [Header("Video Rendering: ")]
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Renderer _screenRenderer;
    [SerializeField] private RenderTexture _targetTexture;
    [Header("Clips: ")]
    [SerializeField] private VideoClip[] _workingClips;
    [SerializeField] private VideoClip[] _slackingClips;

    private void Awake()
    {
        _targetTexture = new RenderTexture(_targetTexture);
        _videoPlayer.targetTexture = _targetTexture;
        _screenRenderer.material.mainTexture = _targetTexture;
    }

    public void SetScreenVideo(ScreenContent state)
    {
        _videoPlayer.gameObject.SetActive(state != ScreenContent.Off);
        VideoClip[] currentClips = null;
        if (state == ScreenContent.Working) currentClips = _workingClips;
        else if (state == ScreenContent.Slacking) currentClips = _slackingClips;
        if (currentClips != null) _videoPlayer.clip = currentClips[Random.Range(0, currentClips.Length)];
    }
}

public enum ScreenContent
{
    Working,
    Slacking,
    Off
}
