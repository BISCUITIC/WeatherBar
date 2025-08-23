using System.Collections;

namespace Localization;

public static class Languages
{
    public static string RU => "ru";
    public static string EN => "en";
    public static string FR => "fr";

    public static IEnumerable<string> Data => [ "en", "ru", "fr" ];
}
