

using System;
using UnityEngine;

public class VNMoment
{
    public enum SpriteAlignment
    {
        Left,
        Center,
        Right
    }

    public string name;
    public string text;
    public Sprite display_sprite;
    public SpriteAlignment display_alignment;
    public Sprite background;
    public AudioClip music_cue;
    public AudioClip voice_line;
    public Action<VNSceneController, float, bool> custom_action;

    VNMoment(string name, string text, Sprite display, SpriteAlignment alignment, Sprite background, AudioClip music, AudioClip voice, Action<VNSceneController, float, bool> custom_action)
    {
        this.name = name;
        this.text = text;
        this.display_sprite = display;
        this.display_alignment = alignment;
        this.background = background;
        this.music_cue = music;
        this.voice_line = voice;
        this.custom_action = custom_action;
    }

    public static VNMoment Create()
    {
        return new VNMoment("", "", null, SpriteAlignment.Left, null, null, null, null);
    }

    public VNMoment Dialogue(string name, string text)
    {
        this.name = name;
        this.text = text;
        return this;
    }

    public VNMoment Display(Sprite display, SpriteAlignment alignment)
    {
        this.display_sprite = display;
        this.display_alignment = alignment;
        return this;
    }

    public VNMoment Background(Sprite background)
    {
        this.background = background;
        return this;
    }

    public VNMoment MusicCue(AudioClip music)
    {
        this.music_cue = music;
        return this;
    }

    public VNMoment VoiceLine(AudioClip voice_line)
    {
        this.voice_line = voice_line;
        return this;
    }

    public VNMoment CustomAction(Action<VNSceneController, float, bool> action)
    {
        this.custom_action = action;
        return this;
    }
}