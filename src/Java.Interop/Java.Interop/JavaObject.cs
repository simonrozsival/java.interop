using System;

namespace Java.Interop
{
	[JniTypeInfo ("java/lang/Object")]
	unsafe public class JavaObject : IJavaPeerable, IJavaPeerableEx
	{
		readonly static JniPeerMembers _members = new JniPeerMembers ("java/lang/Object", typeof (JavaObject));

		int     keyHandle;
		bool    registered;

#if FEATURE_HANDLES_ARE_SAFE_HANDLES
		JniObjectReference  reference;
#endif  // FEATURE_HANDLES_ARE_INTPTRS
#if FEATURE_HANDLES_ARE_INTPTRS
		IntPtr                  handle;
		JniObjectReferenceType  handle_type;
#endif  // FEATURE_HANDLES_ARE_INTPTRS

		protected   static  readonly    JniObjectReference*     InvalidJniObjectReference  = null;

		~JavaObject ()
		{
			JniEnvironment.Current.JavaVM.TryCollectObject (this);
		}

		public          JniObjectReference          PeerReference {
			get {
#if FEATURE_HANDLES_ARE_SAFE_HANDLES
				return reference;
#endif  // FEATURE_HANDLES_ARE_INTPTRS
#if FEATURE_HANDLES_ARE_INTPTRS
				return new JniObjectReference (handle, handle_type);
#endif  // FEATURE_HANDLES_ARE_INTPTRS
			}
		}

		// Note: JniPeerMembers is invoked virtually from the constructor;
		// it MUST be valid before the derived constructor executes!
		// The pattern MUST be followed.
		public  virtual JniPeerMembers              JniPeerMembers {
			get {return _members;}
		}

		public JavaObject (ref JniObjectReference reference, JniHandleOwnership transfer)
		{
			if ((transfer & JniHandleOwnership.Invalid) == JniHandleOwnership.Invalid)
				return;

			using (SetPeerReference (ref reference, transfer)) {
			}
		}

		public unsafe JavaObject ()
		{
			var peer = JniPeerMembers.InstanceMethods.StartCreateInstance ("()V", GetType (), null);
			using (SetPeerReference (
					ref peer,
					JniHandleOwnership.Transfer)) {
				JniPeerMembers.InstanceMethods.FinishCreateInstance ("()V", this, null);
			}
		}

		protected SetPeerReferenceCompletion SetPeerReference (ref JniObjectReference handle, JniHandleOwnership transfer)
		{
			return JniEnvironment.Current.JavaVM.SetObjectPeerReference (
					this,
					ref handle,
					transfer,
					a => new SetPeerReferenceCompletion (a));
		}

		public void RegisterWithVM ()
		{
			JniEnvironment.Current.JavaVM.RegisterObject (this);
		}

		public void Dispose ()
		{
			JniEnvironment.Current.JavaVM.DisposeObject (this);
		}

		public void DisposeUnlessRegistered ()
		{
			if (registered)
				return;
			Dispose ();
		}

		protected virtual void Dispose (bool disposing)
		{
		}

		public override bool Equals (object obj)
		{
			JniPeerMembers.AssertSelf (this);

			if (object.ReferenceEquals (obj, this))
				return true;
			var o = obj as IJavaPeerable;
			if (o != null)
				return JniEnvironment.Types.IsSameObject (PeerReference, o.PeerReference);
			return false;
		}

		public override unsafe int GetHashCode ()
		{
			return _members.InstanceMethods.CallInt32Method ("hashCode\u0000()I", this, null);
		}

		public override unsafe string ToString ()
		{
			var lref = _members.InstanceMethods.CallObjectMethod (
					"toString\u0000()Ljava/lang/String;",
					this,
					null);
			return JniEnvironment.Strings.ToString (ref lref, JniHandleOwnership.Transfer);
		}

		int IJavaPeerableEx.IdentityHashCode {
			get {return keyHandle;}
			set {keyHandle = value;}
		}

		bool IJavaPeerableEx.Registered {
			get {return registered;}
			set {registered = value;}
		}

		void IJavaPeerableEx.Dispose (bool disposing)
		{
			Dispose (disposing);
		}

		void IJavaPeerableEx.SetPeerReference (JniObjectReference reference)
		{
#if FEATURE_HANDLES_ARE_SAFE_HANDLES
			this.reference  = reference;
#endif  // FEATURE_HANDLES_ARE_INTPTRS
#if FEATURE_HANDLES_ARE_INTPTRS
			this.handle         = reference.Handle;
			this.handle_type    = reference.Type;
#endif  // FEATURE_HANDLES_ARE_INTPTRS
		}

		protected struct SetPeerReferenceCompletion : IDisposable {

			readonly    Action  action;

			public SetPeerReferenceCompletion (Action action)
			{
				this.action = action;
			}

			public void Dispose ()
			{
				if (action != null)
					action ();
			}
		}
	}
}

