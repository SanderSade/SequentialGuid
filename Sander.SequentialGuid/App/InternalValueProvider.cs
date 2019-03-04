using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Sander.SequentialGuid.App
{
	internal sealed class InternalValueProvider
	{
		/// <summary>
		///  From https://stackoverflow.com/a/16222886/3248515, modified
		/// </summary>
		private static Func<TS, T> CreateGetter<TS, T>(string fieldName)
		{
			var field = typeof(Guid).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			Debug.Assert(field != null, nameof(field) + " != null");
			Debug.Assert(field.ReflectedType != null, "field.ReflectedType != null");
			var setterMethod = new DynamicMethod($"{field.ReflectedType.FullName}.get_{field.Name}",
				typeof(T), new[]
					{ typeof(TS) }, true);
			var gen = setterMethod.GetILGenerator();
			gen.Emit(OpCodes.Ldarg_0);
			gen.Emit(OpCodes.Ldfld, field);
			gen.Emit(OpCodes.Ret);
			return (Func<TS, T>)setterMethod.CreateDelegate(typeof(Func<TS, T>));
		}
	}
}
