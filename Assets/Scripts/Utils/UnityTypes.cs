namespace UnityTypes
{
    public enum UnityLayer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
    }

    public enum UnityTags
    {
        Untagged,
        Respawn,
        Finish,
        EditorOnly,
        MainCamera,
        Player,
        GameController,
        ThoughtBalloon,
        DialogueManager
    }

    public enum UnityScenes
    {
        StartMenu,
        Bar,
        Minigame,
        BenDive,
        BartenderDive,
        KevinDive,
        TinaDive,
        WallyDive
    }
    
}