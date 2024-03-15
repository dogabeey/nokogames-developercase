using UnityEngine;

public struct Const
{
    public struct Addressables
    {

    }
    public struct Values
    {
        public static float OBJECT_STACK_TWEEN_DURATION = 0.15f;
    }
    public struct GameEvents
    {
        public static string CREATURE_DEATH = "CREATURE_DEATH";
        public static string CREATURE_JUMP = "CREATURE_JUMP";

        public static string PLAYER_CREATED = "PLAYER_CREATED";

        public static string WORKER_FULL = "WORKER_FULL";
        public static string WORKER_EMPTY = "WORKER_EMPTY";
        public static string MACHINE_INPUT_FULL = "MACHINE_INPUT_FULL";
        public static string MACHINE_OUTPUT_EMPTY = "MACHINE_OUTPUT_EMPTY";
    }
}