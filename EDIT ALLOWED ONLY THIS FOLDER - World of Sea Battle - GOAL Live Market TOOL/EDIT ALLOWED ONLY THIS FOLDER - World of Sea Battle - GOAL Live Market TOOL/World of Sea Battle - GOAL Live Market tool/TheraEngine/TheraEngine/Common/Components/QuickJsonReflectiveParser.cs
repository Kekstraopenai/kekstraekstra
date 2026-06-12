using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;

namespace Common.Components
{
	// Token: 0x0200000A RID: 10
	public static class QuickJsonReflectiveParser
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002A50 File Offset: 0x00000C50
		public static List<T> ParseJsonList<T>(JSPContext context, string jsonString) where T : new()
		{
			List<T> list = new List<T>();
			new JsonSerializerOptions().PropertyNameCaseInsensitive = context.CaseInsensitive;
			JsonElement rootElement = JsonDocument.Parse(jsonString, default(JsonDocumentOptions)).RootElement;
			Type typeFromHandle = typeof(T);
			bool flag = typeFromHandle.IsGenericType && typeFromHandle.GetGenericTypeDefinition() == typeof(List<>);
			foreach (JsonElement jsonElement in rootElement.EnumerateArray())
			{
				if (flag)
				{
					object obj = QuickJsonReflectiveParser.DeserializeJsonValue(context, jsonElement, typeFromHandle.GenericTypeArguments.First<Type>().MakeArrayType());
					list.Add((T)((object)Activator.CreateInstance(typeFromHandle, new object[]
					{
						obj
					})));
				}
				else
				{
					T item = (T)((object)QuickJsonReflectiveParser.DeserializeObject(context, typeFromHandle, jsonElement));
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B54 File Offset: 0x00000D54
		private static void Setup(in object obj, FieldInfo property, Type propertyType, object propertyValue)
		{
			if (propertyType == typeof(string))
			{
				string text = (string)propertyValue;
				text = text.Substring(1, text.Length - 2);
				property.SetValue(obj, text);
				return;
			}
			if (propertyType == typeof(int))
			{
				property.SetValue(obj, int.Parse(propertyValue.ToString()));
				return;
			}
			if (propertyType.IsArray)
			{
				property.SetValue(obj, propertyValue);
				return;
			}
			throw new Exception();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BD8 File Offset: 0x00000DD8
		private static object DeserializeObject(JSPContext context, Type type, JsonElement jsonElement)
		{
			object obj = Activator.CreateInstance(type);
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				JsonElement jsonValue;
				if (jsonElement.TryGetProperty(fieldInfo.Name, out jsonValue))
				{
					Type fieldType = fieldInfo.FieldType;
					object value = QuickJsonReflectiveParser.DeserializeJsonValue(context, jsonValue, fieldType);
					fieldInfo.SetValue(obj, value);
				}
			}
			return obj;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C38 File Offset: 0x00000E38
		private static object DeserializeJsonValue(JSPContext context, JsonElement jsonValue, Type targetType)
		{
			if (jsonValue.ValueKind == JsonValueKind.Null)
			{
				return null;
			}
			try
			{
				if (targetType == typeof(string))
				{
					if (jsonValue.ValueKind != JsonValueKind.String && context.ConvertNumbersToString)
					{
						if (context.WriteAlerts)
						{
							context.AlertsOrNull = (context.AlertsOrNull ?? new List<string>());
							context.AlertsOrNull.Add("Type alert: file " + context.Filename + ", value: " + jsonValue.GetRawText());
						}
						return jsonValue.GetRawText();
					}
					return jsonValue.GetString();
				}
				else
				{
					if (targetType == typeof(bool))
					{
						return jsonValue.GetBoolean();
					}
					if (targetType == typeof(int))
					{
						return jsonValue.GetInt32();
					}
					if (targetType == typeof(float))
					{
						return float.Parse(jsonValue.GetRawText(), CultureInfo.InvariantCulture);
					}
					if (targetType == typeof(Vector2))
					{
						return new Vector2Converter().ConvertFrom(null, CultureInfo.InvariantCulture, jsonValue.GetString());
					}
					if (targetType == typeof(Vector3))
					{
						return new Vector3Converter().ConvertFrom(null, CultureInfo.InvariantCulture, jsonValue.GetString());
					}
					if (targetType.IsEnum)
					{
						return Enum.Parse(targetType, jsonValue.GetString());
					}
					if (targetType == typeof(DateTime))
					{
						return jsonValue.GetDateTime();
					}
				}
			}
			catch
			{
				throw new FormatException("Failed to parse " + jsonValue.GetRawText() + ", probably type is wrong, target type: " + targetType.Name);
			}
			if (!targetType.IsArray)
			{
				return QuickJsonReflectiveParser.DeserializeObject(context, targetType, jsonValue);
			}
			Type elementType = targetType.GetElementType();
			if (jsonValue.ValueKind == JsonValueKind.String && jsonValue.GetString().Length == 0)
			{
				return Array.CreateInstance(elementType, 0);
			}
			JsonElement.ArrayEnumerator arrayEnumerator = jsonValue.EnumerateArray();
			Array array = Array.CreateInstance(elementType, arrayEnumerator.Count<JsonElement>());
			int num = 0;
			foreach (JsonElement jsonValue2 in arrayEnumerator)
			{
				object value = QuickJsonReflectiveParser.DeserializeJsonValue(context, jsonValue2, elementType);
				array.SetValue(value, num++);
			}
			return array;
		}
	}
}
