using System;
using System.Collections.Generic;
using Java.Interop;

namespace Xamarin.Test {

	// Metadata.xml XPath class reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']"
	[global::Java.Interop.JniTypeSignature ("xamarin/test/SomeObject", GenerateJavaPeer=false)]
	public partial class SomeObject : global::Java.Lang.Object {

		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='myStrings']"
		public global::Java.Interop.JavaObjectArray<string> MyStrings {
			get {
				const string __id = "myStrings.[Ljava/lang/String;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Interop.JniEnvironment.Runtime.ValueManager.GetValue<global::Java.Interop.JavaObjectArray<string> >(ref __v, JniObjectReferenceOptions.Copy);
			}
			set {
				const string __id = "myStrings.[Ljava/lang/String;";

				try {
					_members.InstanceFields.SetValue (__id, this, value?.PeerReference ?? default);

				} finally {
					GC.KeepAlive (value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='myInts']"
		public global::Java.Interop.JavaInt32Array MyInts {
			get {
				const string __id = "myInts.[I";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Interop.JniEnvironment.Runtime.ValueManager.GetValue<global::Java.Interop.JavaInt32Array >(ref __v, JniObjectReferenceOptions.Copy);
			}
			set {
				const string __id = "myInts.[I";

				try {
					_members.InstanceFields.SetValue (__id, this, value?.PeerReference ?? default);

				} finally {
					GC.KeepAlive (value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='mybools']"
		public global::Java.Interop.JavaBooleanArray Mybools {
			get {
				const string __id = "mybools.[Z";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Interop.JniEnvironment.Runtime.ValueManager.GetValue<global::Java.Interop.JavaBooleanArray >(ref __v, JniObjectReferenceOptions.Copy);
			}
			set {
				const string __id = "mybools.[Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value?.PeerReference ?? default);

				} finally {
					GC.KeepAlive (value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='myObjects']"
		public global::Java.Interop.JavaObjectArray<global::Java.Lang.Object> MyObjects {
			get {
				const string __id = "myObjects.[Ljava/lang/Object;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Interop.JniEnvironment.Runtime.ValueManager.GetValue<global::Java.Interop.JavaObjectArray<global::Java.Lang.Object> >(ref __v, JniObjectReferenceOptions.Copy);
			}
			set {
				const string __id = "myObjects.[Ljava/lang/Object;";

				try {
					_members.InstanceFields.SetValue (__id, this, value?.PeerReference ?? default);

				} finally {
					GC.KeepAlive (value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='myfloats']"
		public global::Java.Interop.JavaSingleArray Myfloats {
			get {
				const string __id = "myfloats.[F";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Interop.JniEnvironment.Runtime.ValueManager.GetValue<global::Java.Interop.JavaSingleArray >(ref __v, JniObjectReferenceOptions.Copy);
			}
			set {
				const string __id = "myfloats.[F";

				try {
					_members.InstanceFields.SetValue (__id, this, value?.PeerReference ?? default);

				} finally {
					GC.KeepAlive (value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='mydoubles']"
		public global::Java.Interop.JavaDoubleArray Mydoubles {
			get {
				const string __id = "mydoubles.[D";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Interop.JniEnvironment.Runtime.ValueManager.GetValue<global::Java.Interop.JavaDoubleArray >(ref __v, JniObjectReferenceOptions.Copy);
			}
			set {
				const string __id = "mydoubles.[D";

				try {
					_members.InstanceFields.SetValue (__id, this, value?.PeerReference ?? default);

				} finally {
					GC.KeepAlive (value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='xamarin.test']/class[@name='SomeObject']/field[@name='mylongs']"
		public global::Java.Interop.JavaInt64Array Mylongs {
			get {
				const string __id = "mylongs.[J";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Interop.JniEnvironment.Runtime.ValueManager.GetValue<global::Java.Interop.JavaInt64Array >(ref __v, JniObjectReferenceOptions.Copy);
			}
			set {
				const string __id = "mylongs.[J";

				try {
					_members.InstanceFields.SetValue (__id, this, value?.PeerReference ?? default);

				} finally {
					GC.KeepAlive (value);
				}
			}
		}

		static readonly JniPeerMembers _members = new JniPeerMembers ("xamarin/test/SomeObject", typeof (SomeObject));

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected SomeObject (ref JniObjectReference reference, JniObjectReferenceOptions options) : base (ref reference, options)
		{
		}

	}
}
