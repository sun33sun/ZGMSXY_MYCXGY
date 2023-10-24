using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ProjectBase
{
    public static class EnumExtension
    {
        public static Type descriptionType = typeof(DescriptionAttribute);
        static Dictionary<Enum, string> _dictionary = new Dictionary<Enum, string>();

        public static string Descriptions<T>(this IEnumerable<T> enumValues) where T : Enum
        {
            StringBuilder builder = new StringBuilder();
            
            Type type = enumValues.First().GetType();
            foreach (T enumValue in enumValues)
            {
                if (!_dictionary.ContainsKey(enumValue))
                {
                    var obj = (DescriptionAttribute)type.GetField(enumValue.ToString())
                        .GetCustomAttribute(descriptionType);
                    _dictionary.Add(enumValue, obj.Description);
                }

                builder.Append(_dictionary[enumValue]).Append("、");
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        public static string Description(this Enum enumValue)
        {
            if (!_dictionary.ContainsKey(enumValue))
            {
                var field = enumValue.GetType().GetField(enumValue.ToString());
                var objs = (DescriptionAttribute)field.GetCustomAttribute(descriptionType);
                _dictionary.Add(enumValue, objs.Description);
            }

            return _dictionary[enumValue];
        }
    }
}