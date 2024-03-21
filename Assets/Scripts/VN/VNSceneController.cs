using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class VNSceneController : MonoBehaviour
{
    VNScene scene;
    VNMoment moment;

    public TMPro.TextMeshProUGUI name_text;
    public TMPro.TextMeshProUGUI dialogue_text;
    public Image[] display_sprite;
    public Image background;

    public AudioSource voice_line_source;
    public AudioSource music_source;
    Action<VNSceneController, float, bool> custom_action;

    bool text_displayed = false;
    string text_now, text_remaining;
    float text_speed = 0.02f;
    float text_timer;

    public void Setup(VNScene scene)
    {
        this.scene = scene;

        ContinueScene();
    }

    void Update()
    {
        if(!text_displayed)
        {
            text_timer -= Time.deltaTime;
            while(text_timer <= 0.0f)
            {
                text_timer += text_speed;
                text_now += text_remaining[0];
                if (text_remaining.Length > 1)
                {
                    text_remaining = text_remaining.Substring(1);
                } 
                else
                {
                    text_displayed = true;
                }
                dialogue_text.SetText(text_now + "<alpha=#00>" + text_remaining);
            }
        }
        if(custom_action != null)
        {
            custom_action(this, Time.deltaTime, text_displayed);
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (text_displayed)
            {
                ContinueScene();
            } 
            else
            {
                CompleteText();    
            }
        }
    }

    public void ContinueScene()
    {        
        if(scene.CanPop())
        {
            moment = scene.Pop();
            DisplayMoment(moment);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
            StageManager.Instance().GetCurrentStage().IncrementStage();
        }
    }

    void CompleteText()
    {
        text_displayed = true;
        dialogue_text.SetText(moment.text);
    }

    void DisplayMoment(VNMoment moment)
    {
        this.moment = moment;
        name_text.SetText("");
        dialogue_text.SetText("");
        voice_line_source.Stop();
        for(int i = 0; i < display_sprite.Length; i++)
        {
            display_sprite[i].gameObject.SetActive(false);
            display_sprite[i].color = Color.white;
        }

        text_displayed = false;

        name_text.SetText(moment.name);

        text_now = "";
        text_remaining = moment.text;
        text_timer = text_speed;

        switch (moment.display_alignment)
        {
            case VNMoment.SpriteAlignment.Left:
                display_sprite[0].gameObject.SetActive(true);
                display_sprite[0].sprite = moment.display_sprite;
                break;
            case VNMoment.SpriteAlignment.Center:
                display_sprite[1].gameObject.SetActive(true);
                display_sprite[1].sprite = moment.display_sprite;
                break;
            case VNMoment.SpriteAlignment.Right:
                display_sprite[2].gameObject.SetActive(true);
                display_sprite[2].sprite = moment.display_sprite;
                break;
        }

        background.sprite = moment.background;

        if (moment.voice_line != null)
        {
            voice_line_source.clip = moment.voice_line;
            voice_line_source.Play();
        }

        if(moment.music_cue != null)
        {
            music_source.clip = moment.music_cue;
            music_source.Play();
        }

        custom_action = moment.custom_action;
        if(custom_action != null)
        {
            custom_action(this, 0, false);
        }
    }
}
