using System.Collections.Generic;

namespace Core.Timeline
{
	class TimelineTrack : ITimelineTrack
	{
		private readonly TimelinePlayer timelinePlayer;

		private readonly Dictionary<int, ITimelineClip> clips = new Dictionary<int, ITimelineClip>();

		private bool isPlaying = false;

		public bool IsPlaying => isPlaying;

		public bool IsEmpty(int time)
		{
			return !clips.ContainsKey(time);
		}

		public void PlayClip(int time)
		{
			if (clips.TryGetValue(time, out var clip))
			{
				clip.Play();
			}
		}

		public void InsertClip(int time, ITimelineClip clip)
		{
			clips[time] = clip;
		}

		public void RemoveClip(int time)
		{
			if (clips.TryGetValue(time, out var clip))
			{
				clip.Dispose();
				clips.Remove(time);
			}
		}

		public void ClearClips()
		{
			foreach (var clip in clips.Values)
			{
				clip.Dispose();
			}

			clips.Clear();
		}

		public TimelineTrack(TimelinePlayer player)
		{
			timelinePlayer = player;
		}
	}
}
