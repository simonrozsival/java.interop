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

		// Metadata.xml XPath constructor reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/constructor[@name='SomeObject' and count(parameter)=0]"
		[Obsolete (@"deprecated")]
		public unsafe SomeObject () : base (ref *InvalidJniObjectReference, JniObjectReferenceOptions.None)
		{
			const string __id = "()V";

			if (PeerReference.IsValid)
				return;

			try {
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), null);
				Construct (ref __r, JniObjectReferenceOptions.CopyAndDispose);
				_members.InstanceMethods.FinishCreateInstance (__id, this, null);
			} finally {
			}
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/constructor[@name='SomeObject' and count(parameter)=1 and parameter[1][@type='int']]"
		public unsafe SomeObject (int aint) : base (ref *InvalidJniObjectReference, JniObjectReferenceOptions.None)
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
