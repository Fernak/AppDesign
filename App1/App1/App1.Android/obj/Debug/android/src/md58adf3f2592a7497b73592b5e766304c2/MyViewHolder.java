package md58adf3f2592a7497b73592b5e766304c2;


public class MyViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("App1.Droid.MyViewHolder, App1.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyViewHolder.class, __md_methods);
	}


	public MyViewHolder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == MyViewHolder.class)
			mono.android.TypeManager.Activate ("App1.Droid.MyViewHolder, App1.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
