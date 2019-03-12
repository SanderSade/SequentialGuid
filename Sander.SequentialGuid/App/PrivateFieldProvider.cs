using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Sander.SequentialGuid.App
{
	/// <summary>
	///     Naive "get private field value" implementation
	/// </summary>
	internal static class PrivateFieldProvider
	{
		private static readonly Lazy<Func<Guid, int>> GetA =
			new Lazy<Func<Guid, int>>(() => GetMethod<Guid, int>("_a"));

		private static readonly Lazy<Func<Guid, short>> GetB =
			new Lazy<Func<Guid, short>>(() => GetMethod<Guid, short>("_b"));

		private static readonly Lazy<Func<Guid, short>> GetC =
			new Lazy<Func<Guid, short>>(() => GetMethod<Guid, short>("_c"));

		private static readonly Lazy<Func<Guid, byte>> GetD =
			new Lazy<Func<Guid, byte>>(() => GetMethod<Guid, byte>("_d"));

		private static readonly Lazy<Func<Guid, byte>> GetE =
			new Lazy<Func<Guid, byte>>(() => GetMethod<Guid, byte>("_e"));

		private static readonly Lazy<Func<Guid, byte>> GetF =
			new Lazy<Func<Guid, byte>>(() => GetMethod<Guid, byte>("_f"));

		private static readonly Lazy<Func<Guid, byte>> GetG =
			new Lazy<Func<Guid, byte>>(() => GetMethod<Guid, byte>("_g"));

		private static readonly Lazy<Func<Guid, byte>> GetH =
			new Lazy<Func<Guid, byte>>(() => GetMethod<Guid, byte>("_h"));

		private static readonly Lazy<Func<Guid, byte>> GetI =
			new Lazy<Func<Guid, byte>>(() => GetMethod<Guid, byte>("_i"));

		private static readonly Lazy<Func<Guid, byte>> GetJ =
			new Lazy<Func<Guid, byte>>(() => GetMethod<Guid, byte>("_j"));

		private static readonly Lazy<Func<Guid, byte>> GetK =
			new Lazy<Func<Guid, byte>>(() => GetMethod<Guid, byte>("_k"));


		internal static byte GetByte(Guid guid, int position)
		{
			switch (position)
			{
				case 0: return (byte)GetA.Value.Invoke(guid);
				case 1: return (byte)(GetA.Value.Invoke(guid) >> 8);
				case 2: return (byte)(GetA.Value.Invoke(guid) >> 16);
				case 3: return (byte)(GetA.Value.Invoke(guid) >> 24);
				case 4: return (byte)GetB.Value.Invoke(guid);
				case 5: return (byte)(GetB.Value.Invoke(guid) >> 8);
				case 6: return (byte)GetC.Value.Invoke(guid);
				case 7: return (byte)(GetC.Value.Invoke(guid) >> 8);
				case 8: return GetD.Value.Invoke(guid);
				case 9: return GetE.Value.Invoke(guid);
				case 10: return GetF.Value.Invoke(guid);
				case 11: return GetG.Value.Invoke(guid);
				case 12: return GetH.Value.Invoke(guid);
				case 13: return GetI.Value.Invoke(guid);
				case 14: return GetJ.Value.Invoke(guid);
				case 15: return GetK.Value.Invoke(guid);
			}

			throw new ArgumentOutOfRangeException();
		}


		private static Func<TGuid, TReturn> GetMethod<TGuid, TReturn>(string fieldName)
		{
			var field = typeof(Guid).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
			Debug.Assert(field != null, nameof(field) + " != null");
			Debug.Assert(field.ReflectedType != null, "field.ReflectedType != null");
			var dynamicMethod = new DynamicMethod(string.Empty, typeof(TReturn), new[] {typeof(TGuid)}, true);
			var ilGenerator = dynamicMethod.GetILGenerator();
			ilGenerator.Emit(OpCodes.Ldarg_0);
			ilGenerator.Emit(OpCodes.Ldfld, field);
			ilGenerator.Emit(OpCodes.Ret);
			return (Func<TGuid, TReturn>)dynamicMethod.CreateDelegate(typeof(Func<TGuid, TReturn>));
		}
	}
}
