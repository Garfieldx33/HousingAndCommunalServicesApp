using System.ComponentModel;

namespace CommonLib.Helpers;

public static class EnumConverter
{
    public static Dictionary<int,string> EnumToDictionary<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(t => (int)(object)t, t => t.ToString());
    }

    public static string GetEnumDescription(Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
    }
}
