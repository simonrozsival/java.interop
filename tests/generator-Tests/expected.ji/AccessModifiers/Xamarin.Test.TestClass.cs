using System;
using System.Collections.Generic;
using Java.Interop;

namespace Xamarin.Test {

	// Metadata.xml XPath class reference: path="/api/package[@name='xamarin.test']/class[@name='TestClass']"
	[global::Java.Interop.JniTypeSignature ("xamarin/test/TestClass", GenerateJavaPeer=false)]
	public partial class TestClass : global::Java.Lang.Object {
		static readonly JniPeerMembers _members = new JniPeerMembers ("xamarin/test/TestClass", typeof (TestClass));

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected TestClass (ref JniObjectReference reference, JniObjectReferenceOptions options) : base (ref reference, options)
		{
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='xamarin.test']/class[@name='TestClass']/constructor[@name='TestClass' and count(parameter)=0]"
		public unsafe TestClass () : base (ref *InvalidJniObjectReference, JniObjectReferenceOptions.None)
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

		// Metadata.xml XPath method reference: path="/api/package[@name='xamarin.test']/class[@name='TestClass']/method[@name='baseMethod' and count(parameter)=0]"
		public virtual unsafe void BaseMethod ()
		{
			const string __id = "baseMethod.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

	}
}
