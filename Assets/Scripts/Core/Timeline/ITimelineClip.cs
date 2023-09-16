using System;

namespace Core.Timeline
{
	public interface ITimelineClip : IDisposable
	{
		bool IsPlaying { get; }

		void Play();

		void Stop();

		void Pause();
	}

	/// <summary>
	/// 동시 재생 가능한 클립. 해당 인터페이스를 상속받으면 동시 재생의 효과를 받는다.
	/// </summary>
	public interface ITimelineParallelClip : ITimelineClip
	{

	}
}
