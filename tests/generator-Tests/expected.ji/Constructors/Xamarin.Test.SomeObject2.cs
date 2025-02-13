using System;
using System.Collections.Generic;
using Java.Interop;

namespace Xamarin.Test {

	// Metadata.xml XPath class reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject2']"
	[global::Java.Interop.JniTypeSignature ("xamarin/test/SomeObject2", GenerateJavaPeer=false)]
	public sealed partial class SomeObject2 : global::Java.Lang.Object {
		static readonly JniPeerMembers _members = new JniPeerMembers ("xamarin/test/SomeObject2", typeof (SomeObject2));

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		internal SomeObject2 (ref JniObjectReference reference, JniObjectReferenceOptions options) : base (ref reference, options)
		{
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject2']/constructor[@name='SomeObject2' and count(parameter)=1 and parameter[1][@type='int']]"
		public unsafe SomeObject2 (int aint) : base (ref *InvalidJniObjectReference, JniObjectReferenceOptions.None)
		{
			const string __id = "(I)V";

			if (PeerReference.IsValid)
				return;

			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (aint);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				Construct (ref __r, JniObjectReferenceOptions.CopyAndDispose);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
			}
		}

	}
}
