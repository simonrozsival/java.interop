using System;
using System.Runtime.InteropServices;

#if INTEROP
namespace Java.Interop
#else
namespace Android.Runtime
#endif
{

	[StructLayout(LayoutKind.Explicit)]
	public struct JValue {
#pragma warning disable 0414
		[FieldOffset(0)] bool z;
		[FieldOffset(0)] sbyte b;
		[FieldOffset(0)] char c;
		[FieldOffset(0)] short s;
		[FieldOffset(0)] int i;
		[FieldOffset(0)] long j;
		[FieldOffset(0)] float f;
		[FieldOffset(0)] double d;
		[FieldOffset(0)] IntPtr l;
#pragma warning restore 0414

		public static JValue Zero = new JValue (IntPtr.Zero);

		public JValue (bool value)
		{
			this = new JValue ();
			z = value;
		}

		public JValue (sbyte value)
		{
			this = new JValue ();
			b = value;
		}

		public JValue (char value)
		{
			this = new JValue ();
			c = value;
		}

		public JValue (short value)
		{
			this = new JValue ();
			s = value;
		}

		public JValue (int value)
		{
			this = new JValue ();
			i = value;
		}

		public JValue (long value)
		{
			this = new JValue ();
			j = value;
		}

		public JValue (float value)
		{
			this = new JValue ();
			f = value;
		}

		public JValue (double value)
		{
			this = new JValue ();
			d = value;
		}

		JValue (IntPtr value)
		{
			this = new JValue ();
			l = value;
		}

		public JValue (JniObjectReference value)
		{
			this = new JValue ();
			l = value.Handle;
		}

		public JValue (IJavaPeerable value)
		{
			this = new JValue ();
			if (value != null)
				l = value.PeerReference.Handle;
			else
				l = IntPtr.Zero;
		}

		public override string ToString ()
		{
			return string.Format ("Java.Interop.JValue(z={0},b={1},c={2},s={3},i={4},f={5},d={6},l=0x{7})",
					z, b, c, s, i, f, d, l.ToString ("x"));
		}
	}
}

