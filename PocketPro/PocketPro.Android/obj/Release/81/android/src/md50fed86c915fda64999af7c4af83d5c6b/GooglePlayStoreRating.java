package md50fed86c915fda64999af7c4af83d5c6b;


public class GooglePlayStoreRating
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("PocketPro.Droid.GooglePlayStoreRating, PocketPro.Android", GooglePlayStoreRating.class, __md_methods);
	}


	public GooglePlayStoreRating ()
	{
		super ();
		if (getClass () == GooglePlayStoreRating.class)
			mono.android.TypeManager.Activate ("PocketPro.Droid.GooglePlayStoreRating, PocketPro.Android", "", this, new java.lang.Object[] {  });
	}

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
