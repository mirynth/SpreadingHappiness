

using UnityEngine;

public class VNScenes
{
    public static VNScene GetScene(int index)
    {
        VNScene scene = new VNScene();

        Sprite Bowba_sprite = Resources.Load<Sprite>("Art/bowba");
        Sprite tappi_sprite = Resources.Load<Sprite>("Art/boba1");
        Sprite enemy_sprite = Resources.Load<Sprite>("Art/angy_vn");

        Color not_talking = new Color(0.6f, 0.6f, 0.6f);

        switch(index)
        {
            //intro
            case 0:
                //This first one chains everything, leaving things null will leave the last moments data.
                //Chair Narration
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Narrator", "Echoes of the past resonate to modern day. The Being of Chaos once again unleashes havoc."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(null, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction(null
                    )); 
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Narrator", "Every single one of your friends are turned against you."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(null, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ));
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Narrator", "It’s time to save the world again with Boba!"
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(null, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ));
                //Protag
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Bowba", "Oh crap I really wished I accepted your contract sooner"
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(Bowba_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction(null
                    ));
                //Tappi
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Tappi", "Now isn’t the time for regrets! We need to focus on saving everyone"
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(tappi_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;
                    }
                    ));
                //Protag
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Bowba", "I know that! What should we do first?"
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(Bowba_sprite, VNMoment.SpriteAlignment.Left
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[1].gameObject.SetActive(true);
                        controller.display_sprite[1].sprite = tappi_sprite;
                        controller.display_sprite[1].color = not_talking;
                    }
                    ));
                //Tappi
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Tappi", "For now you need to get used to using my powers"
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(tappi_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;
                    }
                    ));
                //Angy
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Angry Girl", "URGH GO DIE!!!"
                    ).VoiceLine(Resources.Load<AudioClip>("Audio/VNLines/Intro-Angry_UghGoDie")
                    ).Display(enemy_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;

                        controller.display_sprite[2].gameObject.SetActive(true);
                        controller.display_sprite[2].sprite = tappi_sprite;
                        controller.display_sprite[2].color = not_talking;
                    }
                    ));
                //Tappi
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Tappi", "Speaking of which we can try it out now! No time like the present!"
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(tappi_sprite, VNMoment.SpriteAlignment.Right
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;

                        controller.display_sprite[1].gameObject.SetActive(true);
                        controller.display_sprite[1].sprite = enemy_sprite;
                        controller.display_sprite[1].color = not_talking;
                    }
                    ));
                break;
                //Midphase
            case 1:
                //Protag
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Bowba", "Wow, that was exhausting..."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(Bowba_sprite, VNMoment.SpriteAlignment.Left
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction(null
                    ));
                //Angy
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Angry Girl", "DIE! DIE! DIE!"
                    ).VoiceLine(Resources.Load<AudioClip>("Audio/VNLines/MidPhase-Angry_DieDieDie")
                    ).Display(enemy_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;
                    }
                    ));
                break;
                //win
            case 2:
                //Angy
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Angry Girl", "NO THIS CANNOT BE! I WAS SUPPOSED TO WIN!"
                    //).VoiceLine(Resources.Load<AudioClip>("Audio/VNLines/Intro-Angry_DieDieDie")
                    ).Display(enemy_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;

                        controller.display_sprite[2].gameObject.SetActive(true);
                        controller.display_sprite[2].sprite = tappi_sprite;
                        controller.display_sprite[2].color = not_talking;
                    }
                    ));
                //Tappi
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Tappi", "Heros 1 and evil constructs of the human mind 0!"
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(tappi_sprite, VNMoment.SpriteAlignment.Right
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;

                        controller.display_sprite[1].gameObject.SetActive(true);
                        controller.display_sprite[1].sprite = enemy_sprite;
                        controller.display_sprite[1].color = not_talking;
                    }
                    ));
                //Protag
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Bowba", "Can you stop being weird? Now what to do...."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(Bowba_sprite, VNMoment.SpriteAlignment.Left
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[2].gameObject.SetActive(true);
                        controller.display_sprite[2].sprite = tappi_sprite;
                        controller.display_sprite[2].color = not_talking;

                        controller.display_sprite[1].gameObject.SetActive(true);
                        controller.display_sprite[1].sprite = enemy_sprite;
                        controller.display_sprite[1].color = not_talking;
                    }
                    ));
                //Tappi
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Tappi", "Firstly don’t blame me, blame humanity. I am the way I am literally because of human cognition."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(tappi_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;
                    }
                    ));
                //Tappi
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Tappi", "Secondly, we need to survey the damage and after that we can do what we need to do."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(tappi_sprite, VNMoment.SpriteAlignment.Right
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;
                    }
                    ));
                //Protag
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Bowba", "Well first I need a quick break. Using you sure drained my energy. I need some boba."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(Bowba_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[2].gameObject.SetActive(true);
                        controller.display_sprite[2].sprite = tappi_sprite;
                        controller.display_sprite[2].color = not_talking;
                    }
                    ));
                //Tappi
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Tappi", "I got you covered."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(tappi_sprite, VNMoment.SpriteAlignment.Right
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[1].gameObject.SetActive(true);
                        controller.display_sprite[1].sprite = Bowba_sprite;
                        controller.display_sprite[1].color = not_talking;
                    }
                    ));


                break;
                //lose
            case 3:
                //Angy
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Angry Girl", "HEHEAHAEHEHE JOIN US"
                    //).VoiceLine(Resources.Load<AudioClip>("Audio/VNLines/Intro-Angry_DieDieDie")
                    ).Display(enemy_sprite, VNMoment.SpriteAlignment.Center
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;

                        controller.display_sprite[2].gameObject.SetActive(true);
                        controller.display_sprite[2].sprite = tappi_sprite;
                        controller.display_sprite[2].color = not_talking;
                    }
                    ));
                //Protag
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Bowba", "I'm sorry everyone.. I wasn't strong enough"
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(Bowba_sprite, VNMoment.SpriteAlignment.Left
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[2].gameObject.SetActive(true);
                        controller.display_sprite[2].sprite = tappi_sprite;
                        controller.display_sprite[2].color = not_talking;

                        controller.display_sprite[1].gameObject.SetActive(true);
                        controller.display_sprite[1].sprite = enemy_sprite;
                        controller.display_sprite[1].color = not_talking;
                    }
                    ));
                //Tappi
                scene.AddMoment(scene.PrepareMoment(
                    ).Dialogue("Tappi", "N-no this shouldn't be happening?!?!?!?! Bowba..."
                    //).VoiceLine(Resources.Load<AudioClip>("")
                    ).Display(tappi_sprite, VNMoment.SpriteAlignment.Right
                    ).Background(null
                    ).MusicCue(null
                    ).CustomAction((VNSceneController controller, float delta, bool text_finished) =>
                    {
                        controller.display_sprite[0].gameObject.SetActive(true);
                        controller.display_sprite[0].sprite = Bowba_sprite;
                        controller.display_sprite[0].color = not_talking;

                        controller.display_sprite[1].gameObject.SetActive(true);
                        controller.display_sprite[1].sprite = enemy_sprite;
                        controller.display_sprite[1].color = not_talking;
                    }
                    ));
                break;
        }

        return scene;
    }
}