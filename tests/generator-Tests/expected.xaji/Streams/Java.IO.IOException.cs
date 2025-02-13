using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Java.IO {

	// Metadata.xml XPath class reference: path="/api/package[@name='java.io']/class[@name='IOException']"
	[global::Android.Runtime.Register ("java/io/IOException", DoNotGenerateAcw=true)]
	public abstract partial class IOException : global::Java.Lang.Throwable {
		static readonly JniPeerMembers _members = new XAPeerMembers ("java/io/IOException", typeof (IOException));

		internal static new IntPtr class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override IntPtr ThresholdClass {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		protected IOException (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
		{
		}

		static Delegate cb_printStackTrace;
#pragma warning disable 0169
		static Delegate GetPrintStackTraceHandler ()
		{
			if (cb_printStackTrace == null)
				cb_printStackTrace = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_V) n_PrintStackTrace);
			return cb_printStackTrace;
		}

		static void n_PrintStackTrace (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Java.IO.IOException> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.PrintStackTrace ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='java.io']/class[@name='IOException']/method[@name='printStackTrace' and count(parameter)=0]"
		[Register ("printStackTrace", "()V", "GetPrintStackTraceHandler")]
		public virtual unsafe void PrintStackTrace ()
		{
			const string __id = "printStackTrace.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

	}

	[global::Android.Runtime.Register ("java/io/IOException", DoNotGenerateAcw=true)]
	internal partial class IOExceptionInvoker : IOException {
		public IOExceptionInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer)
		{
		}

		static readonly JniPeerMembers _members = new XAPeerMembers ("java/io/IOException", typeof (IOExceptionInvoker));

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

	}
}
