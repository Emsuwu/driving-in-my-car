using UnityEngine;

public class Meowtils : MonoBehaviour
{
    public static int BoolToInt(bool value)
    {
        return (value ? 1 : 0);
    }
    public static bool IntToBool(int value)
    {
        return value == 1 ? true : false;
    }
}
