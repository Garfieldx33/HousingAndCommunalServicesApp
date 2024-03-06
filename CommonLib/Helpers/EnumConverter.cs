namespace CommonLib.Helpers;

public static class EnumConverter
{
    public static Dictionary<int,string> EnumToDictionary<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(t => (int)(object)t, t => t.ToString());
    }
}
