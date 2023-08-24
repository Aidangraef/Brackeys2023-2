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

    [System.Serializable]
    public enum UnityScenes
    {
        StartMenu,
        Bar,
        Minigame,
        BenDive1,
        BenDive2,
        BenDive3,
        KevinDive1,
        KevinDive2,
        KevinDive3,
        TinaDive1,
        TinaDive2,
        TinaDive3,
        WallyDive1,
        WallyDive2
    }
    
}