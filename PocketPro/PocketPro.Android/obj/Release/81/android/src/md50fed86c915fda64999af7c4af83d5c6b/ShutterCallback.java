package md50fed86c915fda64999af7c4af83d5c6b;


public class ShutterCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.hardware.Camera.ShutterCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onShutter:()V:GetOnShutterHandler:Android.Hardware.Camera/IShutterCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("PocketPro.Droid.ShutterCallback, PocketPro.Android", ShutterCallback.class, __md_methods);
	}


	public ShutterCallback ()
	{
		super ();
		if (getClass () == ShutterCallback.class)
			mono.android.TypeManager.Activate ("PocketPro.Droid.ShutterCallback, PocketPro.Android", "", this, new java.lang.Object[] {  });
	}


	public void onShutter ()
	{
		n_onShutter ();
	}

	private native void n_onShutter ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
