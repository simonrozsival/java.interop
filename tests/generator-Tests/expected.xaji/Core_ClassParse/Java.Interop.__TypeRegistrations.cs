using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Java.Interop {

	partial class __TypeRegistrations {

		public static void RegisterPackages ()
		{
#if MONODROID_TIMING
			var start = DateTime.Now;
			Android.Util.Log.Info ("MonoDroid-Timing", "RegisterPackages start: " + (start - new DateTime (1970, 1, 1)).TotalMilliseconds);
#endif // def MONODROID_TIMING
			Java.Interop.TypeManager.RegisterPackages (
					new string[]{
						"com/com/google/compose",
						"xamarin/test/invalidnames",
					},
					new Converter<string, Type>[]{
						lookup_com_com_google_compose_package,
						lookup_xamarin_test_invalidnames_package,
					});
#if MONODROID_TIMING
			var end = DateTime.Now;
			Android.Util.Log.Info ("MonoDroid-Timing", "RegisterPackages time: " + (end - new DateTime (1970, 1, 1)).TotalMilliseconds + " [elapsed: " + (end - start).TotalMilliseconds + " ms]");
#endif // def MONODROID_TIMING
		}

#if NET5_0_OR_GREATER
		[System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage ("Trimming", "IL2057")]
#endif
		static Type Lookup (string[] mappings, string javaType)
		{
			var managedType = Java.Interop.TypeManager.LookupTypeMapping (mappings, javaType);
			if (managedType == null)
				return null;
			return Type.GetType (managedType);
		}

		static string[] package_com_com_google_compose_mappings;
		static Type lookup_com_com_google_compose_package (string klass)
		{
			if (package_com_com_google_compose_mappings == null) {
				package_com_com_google_compose_mappings = new string[]{
					"com/com/google/compose/MyClass:Xamarin.Google.Composable.MyClass",
				};
			}

			return Lookup (package_com_com_google_compose_mappings, klass);
		}

		static string[] package_xamarin_test_invalidnames_mappings;
		static Type lookup_xamarin_test_invalidnames_package (string klass)
		{
			if (package_xamarin_test_invalidnames_mappings == null) {
				package_xamarin_test_invalidnames_mappings = new string[]{
					"xamarin/test/invalidnames/in:Xamarin.Test.Invalidnames.In",
				};
			}

			return Lookup (package_xamarin_test_invalidnames_mappings, klass);
		}
	}
}
