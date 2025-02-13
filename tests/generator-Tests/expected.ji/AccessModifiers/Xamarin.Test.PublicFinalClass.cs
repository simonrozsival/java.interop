using System;
using System.Collections.Generic;
using Java.Interop;

namespace Xamarin.Test {

	// Metadata.xml XPath class reference: path="/api/package[@name='xamarin.test']/class[@name='PublicFinalClass']"
	[global::Java.Interop.JniTypeSignature ("xamarin/test/PublicFinalClass", GenerateJavaPeer=false)]
	public sealed partial class PublicFinalClass : global::Xamarin.Test.BasePublicClass {
		static readonly JniPeerMembers _members = new JniPeerMembers ("xamarin/test/PublicFinalClass", typeof (PublicFinalClass));

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		internal PublicFinalClass (ref JniObjectReference reference, JniObjectReferenceOptions options) : base (ref reference, options)
		{
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='xamarin.test']/class[@name='PublicFinalClass']/method[@name='publicMethod' and count(parameter)=0]"
		public unsafe void PublicMethod ()
		{
			const string __id = "publicMethod.()V";
			try {
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, null);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='xamarin.test']/class[@name='PackageClassB']/method[@name='packageMethodB' and count(parameter)=0]"
		public unsafe void PackageMethodB ()
		{
			const string __id = "packageMethodB.()V";
			try {
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, null);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='xamarin.test']/class[@name='PackageClassA']/method[@name='packageMethodA' and count(parameter)=0]"
		public unsafe void PackageMethodA ()
		{
			const string __id = "packageMethodA.()V";
			try {
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, null);
			} finally {
			}
		}

	}
}
