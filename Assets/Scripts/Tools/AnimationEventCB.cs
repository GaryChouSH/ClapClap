using UnityEngine;
using System.Collections;

public class AnimationEventCB : MonoBehaviour {

    public delegate void EndEvent();
    public event EndEvent onEndEvent;

    public void setAnimationEndCB(EndEvent cb)
    {
        onEndEvent += cb;
    }

    public void AnimationEnd()
    {
        onEndEvent();
    }

	public delegate void Event1();
	public event Event1 onEvent1;

	public void setAnimation1CB(Event1 cb)
	{
		onEvent1 += cb;
	}

	public void Animation1()
	{
		onEvent1();
	}

	public delegate void Event2();
	public event Event2 onEvent2;

	public void setAnimation2CB(Event2 cb)
	{
		onEvent2 += cb;
	}

	public void Animation2()
	{
		onEvent2();
	}

	//使用下方客制化複數事件代理要執行一次init()
	[SerializeField]
	public CustomAnimationEvent[] customAnimationEvent;
	public void init(){
		customAnimationEvent = new CustomAnimationEvent[5];
	}
	public void setAnimationEventCB(int idx,CustomAnimationEvent.CustomEvent cb)
	{
		customAnimationEvent [idx] = new CustomAnimationEvent ();
		customAnimationEvent [idx].setAnimationEvent (cb);
	}

	public void AnimationDoEvent(int idx)
	{
		customAnimationEvent [idx].AnimationDoEvent ();
	}
}
public class CustomAnimationEvent{

	public delegate void CustomEvent();
	public event CustomEvent onEvent;

	public void setAnimationEvent(CustomEvent cb)
	{
		onEvent += cb;
	}

	public void AnimationDoEvent()
	{
		onEvent();
	}
}