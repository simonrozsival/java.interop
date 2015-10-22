using System;

using Java.Interop;

using NUnit.Framework;

namespace Java.InteropTests
{
	[TestFixture]
	public class JavaVMTest : JavaVMFixture
	{
		[Test]
		public void CreateJavaVM ()
		{
			Assert.AreSame (JavaVM.Current, JavaVM.Current);
			Assert.IsTrue (JavaVM.Current.InvocationPointer != IntPtr.Zero);
			Assert.IsNotNull (JniEnvironment.Current);
		}

#if !__ANDROID__
		[Test]
		public void JDK_OnlySupportsOneVM ()
		{
			try {
				var second = new JreVMBuilder ().CreateJreVM ();
				// If we reach here, we're in a JVM that supports > 1 VM
				second.Dispose ();
				Assert.Ignore ();
			} catch (NotSupportedException) {
			} catch (Exception e){
				Assert.Fail ("Expected NotSupportedException; got: {0}", e);
			}
		}
#endif  // !__ANDROID__

		[Test, ExpectedException (typeof (ArgumentNullException))]
		public void CreateJavaVMWithNullBuilder ()
		{
			new JavaVMWithNullBuilder ();
		}

		class JavaVMWithNullBuilder : JavaVM {
			public JavaVMWithNullBuilder ()
				: base ((JavaVMOptions) null)
			{
			}

			protected override bool TryGC (IJavaPeerable value, ref JniObjectReference handle)
			{
				throw new NotImplementedException ();
			}
		}

		[Test]
		public void GetRegisteredJavaVM_ExistingInstance ()
		{
			Assert.AreEqual (JavaVM.Current, JavaVM.GetRegisteredJavaVM (JavaVM.Current.InvocationPointer));
		}

		[Test]
		public void GetObject_ReturnsAlias ()
		{
			var local   = new JavaObject ();
			Assert.IsNull (JavaVM.Current.PeekObject (local.PeerReference));
			// GetObject must always return a value (unless handle is null, etc.).
			// However, since we didn't call local.RegisterWithVM(),
			// JavaVM.PeekObject() is null (asserted above), but GetObject() must
			// **still** return _something_.
			// In this case, it returns an _alias_.
			// TODO: "most derived type" alias generation. (Not relevant here, but...)
			var p       = local.PeerReference;
			var alias   = JavaVM.Current.GetObject (ref p, JniHandleOwnership.DoNotTransfer);
			Assert.AreNotSame (local, alias);
			alias.Dispose ();
			local.Dispose ();
		}

		[Test]
		public void GetObject_ReturnsNullWithNullHandle ()
		{
			var o = JavaVM.Current.GetObject (IntPtr.Zero);
			Assert.IsNull (o);
		}

		[Test]
		public void GetObject_ReturnsRegisteredInstance ()
		{
			JniObjectReference lref;
			using (var o = new JavaObject ()) {
				lref = o.PeerReference.NewLocalRef ();
				Assert.IsNull (JavaVM.Current.PeekObject (lref));
				o.RegisterWithVM ();
				Assert.AreSame (o, JavaVM.Current.PeekObject (lref));
			}
			// At this point, the Java-side object is kept alive by `lref`,
			// but the wrapper instance has been disposed, and thus should
			// be unregistered, and thus unfindable.
			Assert.IsNull (JavaVM.Current.PeekObject (lref));
			JniEnvironment.Handles.Dispose (ref lref);
		}

		[Test]
		public void GetObject_ReturnsNullWithInvalidSafeHandle ()
		{
			var invalid = new JniObjectReference ();
			Assert.IsNull (JavaVM.Current.GetObject (ref invalid, JniHandleOwnership.Transfer));
		}

		[Test]
		public unsafe void GetObject_FindBestMatchType ()
		{
			using (var t = new JniType (TestType.JniTypeName)) {
				var c = t.GetConstructor ("()V");
				var o = t.NewObject (c, null);
				using (var w = JavaVM.Current.GetObject (ref o, JniHandleOwnership.Transfer)) {
					Assert.AreEqual (typeof (TestType), w.GetType ());
				}
			}
		}

		[Test]
		public void GetJniTypeInfoForType ()
		{
			Assert.Throws<ArgumentNullException> (() => JavaVM.Current.GetJniTypeInfoForType (null));
			Assert.Throws<ArgumentException>(() => JavaVM.Current.GetJniTypeInfoForType (typeof (int[,])));
			Assert.Throws<ArgumentException>(() => JavaVM.Current.GetJniTypeInfoForType (typeof (int[,][])));
			Assert.Throws<ArgumentException>(() => JavaVM.Current.GetJniTypeInfoForType (typeof (int[][,])));
			Assert.Throws<ArgumentException>(() => JavaVM.Current.GetJniTypeInfoForType (typeof (Action<>)));

			AssertGetJniTypeInfoForType (typeof (string),   "java/lang/String",   false,  0);

			AssertGetJniTypeInfoForType (typeof (sbyte),    "B",    true,   0);
			AssertGetJniTypeInfoForType (typeof (short),    "S",    true,   0);
			AssertGetJniTypeInfoForType (typeof (int),      "I",    true,   0);
			AssertGetJniTypeInfoForType (typeof (long),     "J",    true,   0);
			AssertGetJniTypeInfoForType (typeof (float),    "F",    true,   0);
			AssertGetJniTypeInfoForType (typeof (double),   "D",    true,   0);
			AssertGetJniTypeInfoForType (typeof (char),     "C",    true,   0);
			AssertGetJniTypeInfoForType (typeof (bool),     "Z",    true,   0);
			AssertGetJniTypeInfoForType (typeof (void),     "V",    true,   0);

			AssertGetJniTypeInfoForType (typeof (JavaObject),  "java/lang/Object",  false,  0);

			// Enums are their underlying type
			AssertGetJniTypeInfoForType (typeof (StringComparison),     "I",    true,   0);
			AssertGetJniTypeInfoForType (typeof (StringComparison[]),   "[I",   true,   1);
			AssertGetJniTypeInfoForType (typeof (StringComparison[][]), "[[I",  true,   2);

			AssertGetJniTypeInfoForType (typeof (int[]),        "[I",   true,   1);
			AssertGetJniTypeInfoForType (typeof (int[][]),      "[[I",  true,   2);
			AssertGetJniTypeInfoForType (typeof (int[][][]),    "[[[I", true,   3);

			AssertGetJniTypeInfoForType (typeof (JavaSByteArray),       "[B",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaInt16Array),       "[S",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaInt32Array),       "[I",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaInt64Array),       "[J",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaSingleArray),      "[F",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaDoubleArray),      "[D",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaCharArray),        "[C",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaBooleanArray),     "[Z",   true,   1);

			AssertGetJniTypeInfoForType (typeof (JavaPrimitiveArray<SByte>),    "[B",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaPrimitiveArray<Int16>),    "[S",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaPrimitiveArray<Int32>),    "[I",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaPrimitiveArray<Int64>),    "[J",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaPrimitiveArray<Single>),   "[F",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaPrimitiveArray<Double>),   "[D",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaPrimitiveArray<Char>),     "[C",   true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaPrimitiveArray<bool>),     "[Z",   true,   1);

			AssertGetJniTypeInfoForType (typeof (JavaArray<SByte>),    "[B",    true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaArray<Int16>),    "[S",    true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaArray<Int32>),    "[I",    true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaArray<Int64>),    "[J",    true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaArray<Single>),   "[F",    true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaArray<Double>),   "[D",    true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaArray<Char>),     "[C",    true,   1);
			AssertGetJniTypeInfoForType (typeof (JavaArray<bool>),     "[Z",    true,   1);

			AssertGetJniTypeInfoForType (typeof (JavaArray<bool>[]),   "[[Z",  true,   2);

			AssertGetJniTypeInfoForType (typeof (JavaArray<JavaObject>),    "[Ljava/lang/Object;",  false,  1);
			AssertGetJniTypeInfoForType (typeof (JavaArray<int[]>),         "[[I",                  true,   2);
			AssertGetJniTypeInfoForType (typeof (JavaArray<int[]>[]),       "[[[I",                 true,   3);
		}

		static void AssertGetJniTypeInfoForType (Type type, string jniType, bool isKeyword, int arrayRank)
		{
			var info = JavaVM.Current.GetJniTypeInfoForType (type);
			Assert.AreEqual (jniType,   info.ToString ());
			Assert.AreEqual (isKeyword, info.TypeIsKeyword);
			Assert.AreEqual (arrayRank, info.ArrayRank);
		}

		[Test]
		public void GetJniSimplifiedTypeReferenceForType ()
		{
			Assert.Throws<ArgumentNullException> (() => JavaVM.Current.GetJniSimplifiedTypeReferenceForType (null));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetJniSimplifiedTypeReferenceForType (typeof (int[])));
			Assert.AreEqual (null, JavaVM.Current.GetJniSimplifiedTypeReferenceForType (typeof(JavaVMTest)));
		}

		[Test]
		public void GetJniMarshalInfoForType ()
		{
			Assert.Throws<ArgumentNullException> (() => JavaVM.Current.GetJniTypeInfoForType (null));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetJniTypeInfoForType (typeof (Action<>)));

			// not yet implemented...
			// TODO: but should we default to using JavaProxyObject, instead of special-casing in JniMarshal?
			//       Probably not? Means that JavaVM.GetJniTypeInfoForType() subclasses can't "just" check for
			//       null values, but maybe they shouldn't anyway?
			AssertGetJniMarshalInfoForType (typeof (AppDomain));

			AssertGetJniMarshalInfoForPrimitiveType<bool> ("JniBoolean");
			AssertGetJniMarshalInfoForPrimitiveType<char> ("JniCharacter");
			AssertGetJniMarshalInfoForPrimitiveType<short> ("JniShort");
			AssertGetJniMarshalInfoForPrimitiveType<int> ("JniInteger");
			AssertGetJniMarshalInfoForPrimitiveType<long> ("JniLong");
			AssertGetJniMarshalInfoForPrimitiveType<float> ("JniFloat");
			AssertGetJniMarshalInfoForPrimitiveType<double> ("JniDouble");

			AssertGetJniMarshalInfoForType (typeof (string),
					getValue:                   "Strings.ToString",
					createLocalRef:             "Strings.NewString");

			AssertGetJniMarshalInfoForType (typeof (JavaObject),
					getValue:                   "JavaPeerableExtensions.GetValue",
					createLocalRef:             "JavaPeerableExtensions.CreateLocalRef");
			AssertGetJniMarshalInfoForType (typeof (JavaException),
					getValue:                   "JavaPeerableExtensions.GetValue",
					createLocalRef:             "JavaPeerableExtensions.CreateLocalRef");

			AssertGetJniMarshalInfoForPrimitiveArray<JavaBooleanArray, bool> ();
			AssertGetJniMarshalInfoForPrimitiveArray<JavaSByteArray, sbyte> ();
			AssertGetJniMarshalInfoForPrimitiveArray<JavaCharArray, char> ();
			AssertGetJniMarshalInfoForPrimitiveArray<JavaInt16Array, short> ();
			AssertGetJniMarshalInfoForPrimitiveArray<JavaInt32Array, int> ();
			AssertGetJniMarshalInfoForPrimitiveArray<JavaInt64Array, long> ();
			AssertGetJniMarshalInfoForPrimitiveArray<JavaSingleArray, float> ();
			AssertGetJniMarshalInfoForPrimitiveArray<JavaDoubleArray, double> ();

			AssertGetJniMarshalInfoForType (typeof (JavaObjectArray<int>),
					getValue:                   "JavaObjectArray`1.GetValue",
					createLocalRef:             "JavaObjectArray`1.CreateLocalRef",
					createMarshalCollection:    "JavaObjectArray`1.CreateMarshalCollection",
					cleanupMarshalCollection:   "JavaObjectArray`1.CleanupMarshalCollection");
			AssertGetJniMarshalInfoForType (typeof (JavaObjectArray<int[]>),
					getValue:                   "JavaObjectArray`1.GetValue",
					createLocalRef:             "JavaObjectArray`1.CreateLocalRef",
					createMarshalCollection:    "JavaObjectArray`1.CreateMarshalCollection",
					cleanupMarshalCollection:   "JavaObjectArray`1.CleanupMarshalCollection");
			AssertGetJniMarshalInfoForType (typeof (JavaObjectArray<int[][]>),
					getValue:                   "JavaObjectArray`1.GetValue",
					createLocalRef:             "JavaObjectArray`1.CreateLocalRef",
					createMarshalCollection:    "JavaObjectArray`1.CreateMarshalCollection",
					cleanupMarshalCollection:   "JavaObjectArray`1.CleanupMarshalCollection");
			AssertGetJniMarshalInfoForType (typeof (JavaObjectArray<JavaInt32Array>),
					getValue:                   "JavaObjectArray`1.GetValue",
					createLocalRef:             "JavaObjectArray`1.CreateLocalRef",
					createMarshalCollection:    "JavaObjectArray`1.CreateMarshalCollection",
					cleanupMarshalCollection:   "JavaObjectArray`1.CleanupMarshalCollection");
		}

		static void AssertGetJniMarshalInfoForType (Type type, string getValue = null, string createJValue = null, string createLocalRef = null, string createMarshalCollection = null, string cleanupMarshalCollection = null)
		{
			Action<string, Delegate, string> assertMethod = (expected, target, message) => {
				if (expected == null)
					Assert.IsNull (target, message);
				else {
					var m = target.Method;
					Assert.AreEqual (
							expected,
							string.Format ("{0}.{1}", m.DeclaringType.Name, m.Name),
							message);
				}
			};
			var info = JavaVM.Current.GetJniMarshalInfoForType (type);
			assertMethod (getValue,                 info.GetValueFromJni,           "GetValueFromJni");
			assertMethod (createJValue,             info.CreateJValue,              "CreateJValue");
			assertMethod (createLocalRef,           info.CreateLocalRef,            "CreateLocalRef");
			assertMethod (createMarshalCollection,  info.CreateMarshalCollection,   "CreateMarshalCollection");
			assertMethod (cleanupMarshalCollection, info.CleanupMarshalCollection,  "CleanupMarshalCollection");
		}

		static void AssertGetJniMarshalInfoForPrimitiveType<T> (string type)
		{
			AssertGetJniMarshalInfoForType (typeof (T),
					getValue:       type + ".GetValueFromJni",
					createJValue:   type + ".CreateJValue",
					createLocalRef: type + ".CreateLocalRef");
			var info = JavaVM.Current.GetJniMarshalInfoForType (typeof(T));
			info.CreateJValue (default (T));
			var lref = info.CreateLocalRef (default (T));
			Assert.AreEqual (default (T), info.GetValueFromJni (ref lref, JniHandleOwnership.DoNotTransfer, null));
			JniEnvironment.Handles.Dispose (ref lref);
		}

		static void AssertGetJniMarshalInfoForPrimitiveArray<TArray, TElement> ()
		{
			AssertGetJniMarshalInfoForType (typeof (TElement[]),
					getValue:                   typeof (TArray).Name + ".GetValueFromJni",
					createLocalRef:             typeof (TArray).Name + ".CreateLocalRef",
					createMarshalCollection:    typeof (TArray).Name + ".CreateMarshalCollection",
					cleanupMarshalCollection:   typeof (TArray).Name + ".CleanupMarshalCollection");
			AssertGetJniMarshalInfoForType (typeof (JavaArray<TElement>),
					getValue:                   typeof (TArray).Name + ".GetValueFromJni",
					createLocalRef:             typeof (TArray).Name + ".CreateLocalRef",
					createMarshalCollection:    typeof (TArray).Name + ".CreateMarshalCollection",
					cleanupMarshalCollection:   typeof (TArray).Name + ".CleanupMarshalCollection");
			AssertGetJniMarshalInfoForType (typeof (JavaPrimitiveArray<TElement>),
					getValue:                   typeof (TArray).Name + ".GetValueFromJni",
					createLocalRef:             typeof (TArray).Name + ".CreateLocalRef",
					createMarshalCollection:    typeof (TArray).Name + ".CreateMarshalCollection",
					cleanupMarshalCollection:   typeof (TArray).Name + ".CleanupMarshalCollection");
			AssertGetJniMarshalInfoForType (typeof (TArray),
					getValue:                   typeof (TArray).Name + ".GetValueFromJni",
					createLocalRef:             typeof (TArray).Name + ".CreateLocalRef",
					createMarshalCollection:    typeof (TArray).Name + ".CreateMarshalCollection",
					cleanupMarshalCollection:   typeof (TArray).Name + ".CleanupMarshalCollection");
		}

		[Test]
		public void GetTypeForJniTypeRefererence ()
		{
			Assert.Throws<ArgumentNullException> (() => JavaVM.Current.GetTypeForJniTypeRefererence (null));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetTypeForJniTypeRefererence ("java.lang.String"));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetTypeForJniTypeRefererence ("Ljava/lang/String;I"));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetTypeForJniTypeRefererence ("ILjava/lang/String;"));

			Assert.AreEqual (typeof (void),     JavaVM.Current.GetTypeForJniTypeRefererence ("V"));
			Assert.AreEqual (typeof (bool),     JavaVM.Current.GetTypeForJniTypeRefererence ("Z"));
			Assert.AreEqual (typeof (char),     JavaVM.Current.GetTypeForJniTypeRefererence ("C"));
			Assert.AreEqual (typeof (sbyte),    JavaVM.Current.GetTypeForJniTypeRefererence ("B"));
			Assert.AreEqual (typeof (short),    JavaVM.Current.GetTypeForJniTypeRefererence ("S"));
			Assert.AreEqual (typeof (int),      JavaVM.Current.GetTypeForJniTypeRefererence ("I"));
			Assert.AreEqual (typeof (long),     JavaVM.Current.GetTypeForJniTypeRefererence ("J"));
			Assert.AreEqual (typeof (float),    JavaVM.Current.GetTypeForJniTypeRefererence ("F"));
			Assert.AreEqual (typeof (double),   JavaVM.Current.GetTypeForJniTypeRefererence ("D"));
			Assert.AreEqual (typeof (string),   JavaVM.Current.GetTypeForJniTypeRefererence ("java/lang/String"));
			Assert.AreEqual (null,              JavaVM.Current.GetTypeForJniTypeRefererence ("com/example/does/not/exist"));
			Assert.AreEqual (null,              JavaVM.Current.GetTypeForJniTypeRefererence ("Lcom/example/does/not/exist;"));
			Assert.AreEqual (null,              JavaVM.Current.GetTypeForJniTypeRefererence ("[Lcom/example/does/not/exist;"));

			Assert.AreEqual (typeof (JavaPrimitiveArray<bool>),     JavaVM.Current.GetTypeForJniTypeRefererence ("[Z"));
			Assert.AreEqual (typeof (JavaPrimitiveArray<char>),     JavaVM.Current.GetTypeForJniTypeRefererence ("[C"));
			Assert.AreEqual (typeof (JavaPrimitiveArray<sbyte>),    JavaVM.Current.GetTypeForJniTypeRefererence ("[B"));
			Assert.AreEqual (typeof (JavaPrimitiveArray<short>),    JavaVM.Current.GetTypeForJniTypeRefererence ("[S"));
			Assert.AreEqual (typeof (JavaPrimitiveArray<int>),      JavaVM.Current.GetTypeForJniTypeRefererence ("[I"));
			Assert.AreEqual (typeof (JavaPrimitiveArray<long>),     JavaVM.Current.GetTypeForJniTypeRefererence ("[J"));
			Assert.AreEqual (typeof (JavaPrimitiveArray<float>),    JavaVM.Current.GetTypeForJniTypeRefererence ("[F"));
			Assert.AreEqual (typeof (JavaPrimitiveArray<double>),   JavaVM.Current.GetTypeForJniTypeRefererence ("[D"));
			Assert.AreEqual (typeof (JavaObjectArray<string>),      JavaVM.Current.GetTypeForJniTypeRefererence ("[Ljava/lang/String;"));

			Assert.AreEqual (typeof (JavaObjectArray<JavaPrimitiveArray<bool>>),    JavaVM.Current.GetTypeForJniTypeRefererence ("[[Z"));
			Assert.AreEqual (typeof (JavaObjectArray<JavaPrimitiveArray<char>>),    JavaVM.Current.GetTypeForJniTypeRefererence ("[[C"));
			Assert.AreEqual (typeof (JavaObjectArray<JavaPrimitiveArray<sbyte>>),   JavaVM.Current.GetTypeForJniTypeRefererence ("[[B"));
			Assert.AreEqual (typeof (JavaObjectArray<JavaPrimitiveArray<short>>),   JavaVM.Current.GetTypeForJniTypeRefererence ("[[S"));
			Assert.AreEqual (typeof (JavaObjectArray<JavaPrimitiveArray<int>>),     JavaVM.Current.GetTypeForJniTypeRefererence ("[[I"));
			Assert.AreEqual (typeof (JavaObjectArray<JavaPrimitiveArray<long>>),    JavaVM.Current.GetTypeForJniTypeRefererence ("[[J"));
			Assert.AreEqual (typeof (JavaObjectArray<JavaPrimitiveArray<float>>),   JavaVM.Current.GetTypeForJniTypeRefererence ("[[F"));
			Assert.AreEqual (typeof (JavaObjectArray<JavaPrimitiveArray<double>>),  JavaVM.Current.GetTypeForJniTypeRefererence ("[[D"));
			Assert.AreEqual (typeof (JavaObjectArray<JavaObjectArray<string>>),     JavaVM.Current.GetTypeForJniTypeRefererence ("[[Ljava/lang/String;"));

			// Yes, these look weird...
			// Assume: class II {}
			Assert.AreEqual (null, JavaVM.Current.GetTypeForJniTypeRefererence ("II"));
			// Assume: package Ljava.lang; class String {}
			Assert.AreEqual (null, JavaVM.Current.GetTypeForJniTypeRefererence ("Ljava/lang/String"));
		}

		[Test]
		public void GetJniTypeInfoForJniTypeReference ()
		{
			Assert.Throws<ArgumentNullException> (() => JavaVM.Current.GetJniTypeInfoForJniTypeReference (null));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetJniTypeInfoForJniTypeReference ("java.lang.String"));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetJniTypeInfoForJniTypeReference ("Ljava/lang/String;I"));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetJniTypeInfoForJniTypeReference ("ILjava/lang/String;"));

			AssertGetJniTypeInfoForJniTypeReference ("java/lang/String",    "java/lang/String");
			AssertGetJniTypeInfoForJniTypeReference ("Ljava/lang/String;",  "java/lang/String");
			AssertGetJniTypeInfoForJniTypeReference ("[I",                  "I",                true,   1);
			AssertGetJniTypeInfoForJniTypeReference ("[[I",                 "I",                true,   2);
			AssertGetJniTypeInfoForJniTypeReference ("[Ljava/lang/Object;", "java/lang/Object", false,  1);

			// Yes, these look _really_ weird...
			// Assume: class II {}
			AssertGetJniTypeInfoForJniTypeReference ("II",                  "II");
			// Assume: package Ljava.lang; class String {}
			AssertGetJniTypeInfoForJniTypeReference ("Ljava/lang/String",   "Ljava/lang/String");
		}

		static void AssertGetJniTypeInfoForJniTypeReference (string jniTypeReference, string jniTypeName, bool typeIsKeyword = false, int arrayRank = 0)
		{
			var info = JavaVM.Current.GetJniTypeInfoForJniTypeReference (jniTypeReference);
			Assert.AreEqual (jniTypeName,   info.JniTypeName,   "JniTypeName for: " + jniTypeReference);
			Assert.AreEqual (typeIsKeyword, info.TypeIsKeyword, "TypeIsKeyword for: " + jniTypeReference);
			Assert.AreEqual (arrayRank,     info.ArrayRank,     "ArrayRank for: " + jniTypeReference);
		}

		[Test]
		public void GetTypeForJniSimplifiedTypeReference ()
		{
			Assert.Throws<ArgumentNullException> (() => JavaVM.Current.GetTypeForJniSimplifiedTypeReference (null));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("foo.bar"));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("[I"));
			Assert.Throws<ArgumentException> (() => JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("Ljava/lang/String;"));

			Assert.AreEqual (typeof (void),     JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("V"));
			Assert.AreEqual (typeof (bool),     JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("Z"));
			Assert.AreEqual (typeof (char),     JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("C"));
			Assert.AreEqual (typeof (sbyte),    JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("B"));
			Assert.AreEqual (typeof (short),    JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("S"));
			Assert.AreEqual (typeof (int),      JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("I"));
			Assert.AreEqual (typeof (long),     JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("J"));
			Assert.AreEqual (typeof (float),    JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("F"));
			Assert.AreEqual (typeof (double),   JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("D"));
			Assert.AreEqual (typeof (string),   JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("java/lang/String"));
			Assert.AreEqual (null,              JavaVM.Current.GetTypeForJniSimplifiedTypeReference ("com/example/does/not/exist"));
		}
	}
}

