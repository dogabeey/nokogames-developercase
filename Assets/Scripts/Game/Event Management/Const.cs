using UnityEngine;

public struct Const
{

    public enum Screens
    {
        MainMenu,
        LevelList,
        WorldList,
        GameScene,
        PauseMenu
    }
    public struct Values
    {
        public static float PICKUP_DROP_HEIGHT_TRY_STEP = 1f;
        public static float MOVEMENT_OVERLAP_SPHERE_SENSITIVITY = 0.2f;
        public static float MOVEMENT_DURATION = 0.2f;
    }

    public struct TAGS
    {
        public static string PLAYER = "Player";
        public static string ENEMY = "Enemy";
        public static string COLLECTIBLE = "Collectible";
        public static string GROUND = "Ground";
    }

    public struct BindingNames
    {
        public static string KEYBOARD = "Keyboard";
        public static string GAMEPAD = "Gamepad";
    }
    public struct SOUNDS
    {
        public struct MUSICS
        {
            public static string MAIN_MENU = "MainMenu";
            public static string GAMEPLAY = "Gameplay";
        }
        public struct EFFECTS
        {
            public static string TYPEWRITER = "Typewriter";
            public static string JUMP = "Jump";
            public static string DEATH = "Death";
            public static string PICKUP = "Pickup";
            public static string LEVEL_COMPLETE = "LevelComplete";
            public static string LEVEL_FAILED = "LevelFailed";
        }
    }

    public struct GameEvents
    {
        public static string ENTITY_MOVED = "ENTITY_MOVED";
        public static string CREATURE_DEATH = "CREATURE_DEATH";
        public static string CREATURE_JUMP = "CREATURE_JUMP";
        public static string COLLECTIBLE_EARNED = "COLLECTIBLE_EARNED";
        public static string OBJECTIVE_COMPLETED = "OBJECTIVE_COMPLETED";
        public static string OBJECTIVE_FAILED = "OBJECTIVE_FAILED";

        public static string LEVEL_COMPLETED = "LEVEL_COMPLETED";
        public static string LEVEL_FAILED = "LEVEL_FAILED";
        public static string LEVEL_STARTED = "LEVEL_STARTED";
        public static string LEVEL_LOCKED = "LEVEL_LOCKED";
        public static string LEVEL_UNLOCKED = "LEVEL_UNLOCKED";

        public static string PLAYER_CREATED = "PLAYER_CREATED";

        public static string PLAYER_ENTERED_RANGE = "PLAYER_ENTERED_RANGE";
        public static string PLAYER_EXITED_RANGE = "PLAYER_EXITED_RANGE";
        public static string PLAYER_PICKED_OBJECT = "PLAYER_PICKED_OBJECT";
        public static string PLAYER_DROPPED_OBJECT = "PLAYER_DROPPED_OBJECT";

        public static string CURRENT_WORLD_CHANGED = "CURRENT_WORLD_CHANGED";
    }
}