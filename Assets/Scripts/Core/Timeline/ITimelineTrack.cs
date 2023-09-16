namespace Core.Timeline
{
	public interface ITimelineTrack
	{
		/// <summary>
		/// 플레이 중인지 검사
		/// </summary>
		bool IsPlaying { get; }

		/// <summary>
		/// 해당 시간에 클립이 비어 있는지 검사
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		bool IsEmpty(int time);

		/// <summary>
		/// 해당 시간의 클립 재생
		/// </summary>
		/// <param name="time"></param>
		void PlayClip(int time);

		/// <summary>
		/// 해당 시간에 클립 추가
		/// </summary>
		/// <param name="time"></param>
		/// <param name="clip"></param>
		void InsertClip(int time, ITimelineClip clip);

		/// <summary>
		/// 해당 시간의 클립 제거
		/// </summary>
		/// <param name="time"></param>
		void RemoveClip(int time);

		/// <summary>
		/// 모든 클립 제거
		/// </summary>
		void ClearClips();
	}
}
