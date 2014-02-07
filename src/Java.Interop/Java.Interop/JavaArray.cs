using System;
using System.Collections;
using System.Collections.Generic;

namespace Java.Interop
{
	public abstract class JavaArray<T> : JavaObject, IList, IList<T>
	{
		internal JavaArray (JniReferenceSafeHandle handle, JniHandleOwnership transfer)
			: base (handle, transfer)
		{
		}

		public int Length {
			get {return JniEnvironment.Arrays.GetArrayLength (SafeHandle);}
		}

		public abstract T this [int index] {
			get;
			set;
		}

		public virtual int IndexOf (T item)
		{
			int len = Length;
			for (int i = 0; i < len; i++)
				if (EqualityComparer<T>.Default.Equals (item, this [i]))
					return i;
			return -1;
		}

		public virtual void Clear ()
		{
			int len = Length;
			for (int i = 0; i < len; i++)
				this [i] = default (T);
		}

		public virtual bool Contains (T item)
		{
			return IndexOf (item) >= 0;
		}

		public virtual void CopyTo (T[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			CheckArrayCopy (0, Length, arrayIndex, array.Length, Length);
			int len = Length;
			for (int i = 0; i < len; i++)
				array [arrayIndex + i] = this [i];
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		public T[] ToArray ()
		{
			var a = new T [Length];
			CopyTo (a, 0);
			return a;
		}

		public virtual IEnumerator<T> GetEnumerator ()
		{
			for (int i = 0; i < Length; ++i)
				yield return this [i];
		}

		internal static void CheckArrayCopy (int sourceIndex, int sourceLength, int destinationIndex, int destinationLength, int length)
		{
			if (sourceIndex < 0)
				throw new ArgumentOutOfRangeException ("sourceIndex", "source index must be >= 0; was " + sourceIndex + ".");
			if (sourceIndex != 0 && sourceIndex >= sourceLength)
				throw new ArgumentException ("source index is > source length.", "sourceIndex");
			if (checked(sourceIndex + length) > sourceLength)
				throw new ArgumentException ("source index + length >= source length", "length");
			if (destinationIndex < 0)
				throw new ArgumentOutOfRangeException ("destinationIndex", "destination index must be >= 0; was " + destinationIndex + ".");
			if (destinationIndex != 0 && destinationIndex >= destinationLength)
				throw new ArgumentException ("destination index is > destination length.", "destinationIndex");
			if (checked (destinationIndex + length) > destinationLength)
				throw new ArgumentException ("destination index + length >= destination length", "length");
		}

		bool ICollection.IsSynchronized {
			get {
				return false;
			}
		}

		object ICollection.SyncRoot {
			get {
				return this;
			}
		}

		int ICollection<T>.Count {
			get {return Length;}
		}

		int ICollection.Count {
			get {return Length;}
		}

		bool IList.IsFixedSize {
			get {
				return true;
			}
		}

		object IList.this [int index] {
			get {return this [index];}
			set {this [index] = (T) value;}
		}

		void ICollection.CopyTo (Array array, int index)
		{
			if (array == null)
				throw new ArgumentNullException ("array");
			CheckArrayCopy (0, Length, index, array.Length, Length);
			int len = Length;
			for (int i = 0; i < len; i++)
				array.SetValue (this [i], index + i);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		void ICollection<T>.Add (T item)
		{
			throw new NotSupportedException ();
		}

		bool ICollection<T>.Remove (T item)
		{
			throw new NotSupportedException ();
		}

		bool IList.Contains (object value)
		{
			if (value is T)
				return Contains ((T) value);
			return false;
		}

		int IList.IndexOf (object value)
		{
			if (value is T)
				return IndexOf ((T) value);
			return -1;
		}

		int IList.Add (object value)
		{
			throw new NotSupportedException ();
		}

		void IList.Insert (int index, object value)
		{
			throw new NotSupportedException ();
		}

		void IList.Remove (object value)
		{
			throw new NotSupportedException ();
		}

		void IList.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}

		void IList<T>.Insert (int index, T item)
		{
			throw new NotSupportedException ();
		}

		void IList<T>.RemoveAt (int index)
		{
			throw new NotSupportedException ();
		}
	}

	[JniTypeInfo ("Z", ArrayRank=1, TypeIsKeyword=true)]
	partial class JavaBooleanArray  {}

	[JniTypeInfo ("B", ArrayRank=1, TypeIsKeyword=true)]
	partial class JavaSByteArray    {}

	[JniTypeInfo ("C", ArrayRank=1, TypeIsKeyword=true)]
	partial class JavaCharArray     {}

	[JniTypeInfo ("S", ArrayRank=1, TypeIsKeyword=true)]
	partial class JavaInt16Array    {}

	[JniTypeInfo ("I", ArrayRank=1, TypeIsKeyword=true)]
	partial class JavaInt32Array    {}

	[JniTypeInfo ("J", ArrayRank=1, TypeIsKeyword=true)]
	partial class JavaInt64Array    {}

	[JniTypeInfo ("F", ArrayRank=1, TypeIsKeyword=true)]
	partial class JavaSingleArray   {}

	[JniTypeInfo ("D", ArrayRank=1, TypeIsKeyword=true)]
	partial class JavaDoubleArray   {}

	public enum JniArrayElementsReleaseMode {
		CopyBack        = 0,
		DoNotCopyBack   = 2
	}

	public abstract class JniArrayElements : IDisposable {

		internal const int JNI_COMMIT = 1;

		IntPtr elements;

		internal JniArrayElements (IntPtr elements)
		{
			if (elements == IntPtr.Zero)
				throw new ArgumentException ("'elements' must not be IntPtr.Zero.", "elements");
			this.elements = elements;
		}

		bool IsDisposed {
			get {return elements == IntPtr.Zero;}
		}

		public  IntPtr  Elements {
			get {
				if (IsDisposed)
					throw new ObjectDisposedException (GetType ().FullName);
				return elements;
			}
		}

		protected   abstract    void    Synchronize (JniArrayElementsReleaseMode releaseMode);

		public void CopyToJava ()
		{
			Synchronize ((JniArrayElementsReleaseMode) JNI_COMMIT);
		}

		public void Release (JniArrayElementsReleaseMode releaseMode)
		{
			if (IsDisposed)
				throw new ObjectDisposedException (GetType ().FullName);;
			Synchronize (releaseMode);
			elements = IntPtr.Zero;
		}

		public void Dispose ()
		{
			if (IsDisposed)
				return;
			Release (JniArrayElementsReleaseMode.CopyBack);
		}
	}
	
	public abstract class JavaPrimitiveArray<T> : JavaArray<T> {

		internal JavaPrimitiveArray (JniReferenceSafeHandle handle, JniHandleOwnership transfer)
			: base (handle, transfer)
		{
		}

		public      abstract    void    CopyTo (int sourceIndex, T[] destinationArray, int destinationIndex, int length);
		public      abstract    void    CopyFrom (T[] sourceArray, int sourceIndex, int destinationIndex, int length);

		protected   abstract    JniArrayElements   CreateElements ();

		public override T this [int index] {
			get {
				var buf = new T [1];
				CopyTo (index, buf, 0, buf.Length);
				return buf [0];
			}
			set {
				if (index >= Length)
					throw new ArgumentOutOfRangeException ("index", "index >= Length");
				var buf = new T []{ value };
				CopyFrom (buf, 0, index, buf.Length);
			}
		}

		public JniArrayElements GetElements ()
		{
			return CreateElements ();
		}

		public override void CopyTo (T[] array, int arrayIndex)
		{
			CopyTo (0, array, arrayIndex, Length);
		}
	}
}

