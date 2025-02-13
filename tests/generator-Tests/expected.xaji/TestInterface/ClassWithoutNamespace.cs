using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

// Metadata.xml XPath class reference: path="/api/package[@name='']/class[@name='ClassWithoutNamespace']"
[global::Android.Runtime.Register ("ClassWithoutNamespace", DoNotGenerateAcw=true)]
public abstract partial class ClassWithoutNamespace : global::Java.Lang.Object, IInterfaceWithoutNamespace {
	static readonly JniPeerMembers _members = new XAPeerMembers ("ClassWithoutNamespace", typeof (ClassWithoutNamespace));

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

	protected ClassWithoutNamespace (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
	{
	}

	// Metadata.xml XPath constructor reference: path="/api/package[@name='']/class[@name='ClassWithoutNamespace']/constructor[@name='ClassWithoutNamespace' and count(parameter)=0]"
	[Register (".ctor", "()V", "")]
	public unsafe ClassWithoutNamespace () : base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
	{
		const string __id = "()V";

		if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
			return;

		try {
			var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), null);
			SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
			_members.InstanceMethods.FinishCreateInstance (__id, this, null);
		} finally {
		}
	}

	static Delegate cb_Foo;
#pragma warning disable 0169
	static Delegate GetFooHandler ()
	{
		if (cb_Foo == null)
			cb_Foo = JNINativeWrapper.CreateDelegate ((_JniMarshal_PP_V) n_Foo);
		return cb_Foo;
	}

	static void n_Foo (IntPtr jnienv, IntPtr native__this)
	{
		var __this = global::Java.Lang.Object.GetObject<ClassWithoutNamespace> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
		__this.Foo ();
	}
#pragma warning restore 0169

	// Metadata.xml XPath method reference: path="/api/package[@name='']/interface[@name='InterfaceWithoutNamespace']/method[@name='Foo' and count(parameter)=0]"
	[Register ("Foo", "()V", "GetFooHandler")]
	public abstract void Foo ();

}

[global::Android.Runtime.Register ("ClassWithoutNamespace", DoNotGenerateAcw=true)]
internal partial class ClassWithoutNamespaceInvoker : ClassWithoutNamespace {
	public ClassWithoutNamespaceInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer)
	{
	}

	static readonly JniPeerMembers _members = new XAPeerMembers ("ClassWithoutNamespace", typeof (ClassWithoutNamespaceInvoker));

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

	// Metadata.xml XPath method reference: path="/api/package[@name='']/interface[@name='InterfaceWithoutNamespace']/method[@name='Foo' and count(parameter)=0]"
	[Register ("Foo", "()V", "GetFooHandler")]
	public override unsafe void Foo ()
	{
		const string __id = "Foo.()V";
		try {
			_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, null);
		} finally {
		}
	}

}
