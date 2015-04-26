package backgroundservice;


public class ReceiverService
	extends android.app.Service
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBind:(Landroid/content/Intent;)Landroid/os/IBinder;:GetOnBind_Landroid_content_Intent_Handler\n" +
			"n_onStart:(Landroid/content/Intent;I)V:GetOnStart_Landroid_content_Intent_IHandler\n" +
			"";
		mono.android.Runtime.register ("BackGroundService.ReceiverService, BackGroundService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ReceiverService.class, __md_methods);
	}


	public ReceiverService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ReceiverService.class)
			mono.android.TypeManager.Activate ("BackGroundService.ReceiverService, BackGroundService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.os.IBinder onBind (android.content.Intent p0)
	{
		return n_onBind (p0);
	}

	private native android.os.IBinder n_onBind (android.content.Intent p0);


	public void onStart (android.content.Intent p0, int p1)
	{
		n_onStart (p0, p1);
	}

	private native void n_onStart (android.content.Intent p0, int p1);

	java.util.ArrayList refList;
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
