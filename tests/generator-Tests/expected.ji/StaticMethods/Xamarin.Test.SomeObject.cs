using System;
using System.Collections.Generic;
using Java.Interop;

namespace Xamarin.Test {

	// Metadata.xml XPath class reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']"
	[global::Java.Interop.JniTypeSignature ("xamarin/test/SomeObject", GenerateJavaPeer=false)]
	public partial class SomeObject : global::Java.Lang.Object {
		static readonly JniPeerMembers _members = new JniPeerMembers ("xamarin/test/SomeObject", typeof (SomeObject));

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected SomeObject (ref JniObjectReference reference, JniObjectReferenceOptions options) : base (ref reference, options)
		{
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/method[@name='methodAsInt' and count(parameter)=0]"
		public static unsafe int MethodAsInt ()
		{
			const string __id = "methodAsInt.()I";
			try {
				var __rm = _members.StaticMethods.InvokeInt32Method (__id, null);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/method[@name='methodAsString' and count(parameter)=0]"
		public static unsafe string MethodAsString ()
		{
			const string __id = "methodAsString.()Ljava/lang/String;";
			try {
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, null);
				return global::Java.Interop.JniEnvironment.Strings.ToString (ref __rm, JniObjectReferenceOptions.CopyAndDispose);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/method[@name='Obsoletemethod' and count(parameter)=0]"
		[Obsolete (@"Deprecated please use methodAsString")]
		public static unsafe string Obsoletemethod ()
		{
			const string __id = "Obsoletemethod.()Ljava/lang/String;";
			try {
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, null);
				return global::Java.Interop.JniEnvironment.Strings.ToString (ref __rm, JniObjectReferenceOptions.CopyAndDispose);
			} finally {
			}
		}

	}
}
