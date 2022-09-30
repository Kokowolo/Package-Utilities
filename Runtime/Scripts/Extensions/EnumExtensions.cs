/*
 * File Name: EnumExtensions.cs
 * Description: This script is for extension functionality regarding Enums
 * 
 * Author(s): Kokowolo, Will Lacey
 * Date Created: September 30, 2022
 * 
 * Additional Comments:
 *		File Line Length: 120
 *
 *      Relates to EnumUtils.cs
 */

using System;
using System.Reflection;
using System.ComponentModel;

public static class EnumExtensions
{
    /************************************************************/
    #region Functions

    public static string ToStringFromPascalCase(this Enum value)
    {
        string str = "";
        Type type = value.GetType();
        string name = Enum.GetName(type, value);
        for (int i = 1; i <= name.Length; i++)
        {
            str += name[i - 1];
            if (i < name.Length && Char.IsUpper(name[i])) str += " ";   
        }
        return str;
    }

    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();
        string name = Enum.GetName(type, value);
        if (name != null)
        {
            FieldInfo field = type.GetField(name);
            if (field != null)
            {
                DescriptionAttribute attr = 
                    Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr != null) return attr.Description;
            }
        }
        return value.ToString();
    }
    
    #endregion
    /************************************************************/
}