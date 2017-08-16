package md5a82996dd0966f6970d5c053946e47aad;


public class ExerciseViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("EpicWorkout.Droid.Fragments.ExerciseItems.ExerciseViewHolder, EpicWorkout.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ExerciseViewHolder.class, __md_methods);
	}


	public ExerciseViewHolder (android.view.View p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == ExerciseViewHolder.class)
			mono.android.TypeManager.Activate ("EpicWorkout.Droid.Fragments.ExerciseItems.ExerciseViewHolder, EpicWorkout.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
