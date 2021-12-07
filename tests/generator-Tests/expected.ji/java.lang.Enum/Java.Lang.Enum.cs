using System;
using System.Collections.Generic;
using Java.Interop;

namespace Java.Lang {

	// Metadata.xml XPath class reference: path="/api/package[@name='java.lang']/class[@name='Enum']"
	[global::Java.Interop.JniTypeSignature ("java/lang/Enum", GenerateJavaPeer=false)]
	[global::Java.Interop.JavaTypeParameters (new string [] {"E extends java.lang.Enum<E>"})]
	public abstract partial class Enum : global::Java.Lang.Object, global::Java.Lang.IComparable {
		static readonly JniPeerMembers _members = new JniPeerMembers ("java/lang/Enum", typeof (Enum));

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected Enum (ref JniObjectReference reference, JniObjectReferenceOptions options) : base (ref reference, options)
		{
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='java.lang']/class[@name='Enum']/method[@name='compareTo' and count(parameter)=1 and parameter[1][@type='E']]"
		public unsafe int CompareTo (global::Java.Lang.Object o)
		{
			const string __id = "compareTo.(Ljava/lang/Enum;)I";
			IntPtr native_o = JNIEnv.ToLocalJniHandle (o);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_o);
				var __rm = _members.InstanceMethods.InvokeNonvirtualInt32Method (__id, this, __args);
				return __rm;
			} finally {
				JNIEnv.DeleteLocalRef (native_o);
				global::System.GC.KeepAlive (o);
			}
		}

	}

	[global::Java.Interop.JniTypeSignature ("java/lang/Enum", GenerateJavaPeer=false)]
	internal partial class EnumInvoker : Enum, global::Java.Lang.IComparable {
		public EnumInvoker (ref JniObjectReference reference, JniObjectReferenceOptions options) : base (ref reference, options)
		{
		}

		static readonly JniPeerMembers _members = new JniPeerMembers ("java/lang/Enum", typeof (EnumInvoker));

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

	}
}
