package md50fed86c915fda64999af7c4af83d5c6b;


public class AutofocusCallBack
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.hardware.Camera.AutoFocusCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAutoFocus:(ZLandroid/hardware/Camera;)V:GetOnAutoFocus_ZLandroid_hardware_Camera_Handler:Android.Hardware.Camera/IAutoFocusCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("PocketPro.Droid.AutofocusCallBack, PocketPro.Android", AutofocusCallBack.class, __md_methods);
	}


	public AutofocusCallBack ()
	{
		super ();
		if (getClass () == AutofocusCallBack.class)
			mono.android.TypeManager.Activate ("PocketPro.Droid.AutofocusCallBack, PocketPro.Android", "", this, new java.lang.Object[] {  });
	}

	public AutofocusCallBack (md50fed86c915fda64999af7c4af83d5c6b.CameraPreview p0)
	{
		super ();
		if (getClass () == AutofocusCallBack.class)
			mono.android.TypeManager.Activate ("PocketPro.Droid.AutofocusCallBack, PocketPro.Android", "PocketPro.Droid.CameraPreview, PocketPro.Android", this, new java.lang.Object[] { p0 });
	}


	public void onAutoFocus (boolean p0, android.hardware.Camera p1)
	{
		n_onAutoFocus (p0, p1);
	}

	private native void n_onAutoFocus (boolean p0, android.hardware.Camera p1);

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
