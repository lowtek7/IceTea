using System.Collections.Generic;
using Core.Timeline;

namespace Core.Timeline
{
	public class TimelinePlayer
	{
		private int currentTimeCursor = 0;

		private bool isPlaying;

		private bool canParallel = false;

		private readonly List<ITimelineTrack> tracks = new List<ITimelineTrack>();

		public bool IsPlaying => isPlaying;

		private ITimelineTrack GetEmptyTrackByTime(int time)
		{
			foreach (var track in tracks)
			{
				if (track.IsEmpty(time))
					return track;
			}

			// allocate track.
			var resultTrack = new TimelineTrack(this);
			tracks.Add(resultTrack);
			return resultTrack;
		}

		public void Update(float deltaTime)
		{
			if (IsPlaying)
			{
				var canNext = true;

				foreach (var track in tracks)
				{
					if (!track.IsPlaying)
					{
						canNext = false;
						break;
					}
				}

				if (canNext)
				{
					PlayTime(currentTimeCursor + 1);
				}
			}
		}

		public bool Play()
		{
			if (IsPlaying)
			{
				return false;
			}

			canParallel = false;
			currentTimeCursor = 0;

			PlayTime(currentTimeCursor);

			return true;
		}

		private void PlayTime(int timeCursor)
		{
			var canPlay = false;

			currentTimeCursor = timeCursor;

			foreach (var track in tracks)
			{
				if (!track.IsEmpty(currentTimeCursor))
				{
					canPlay = true;
					break;
				}
			}

			if (!canPlay)
			{
				Clear();
			}
			else
			{
				isPlaying = true;

				foreach (var track in tracks)
				{
					track.PlayClip(currentTimeCursor);
				}
			}
		}

		public void AddClip(ITimelineClip clip)
		{
			if (IsPlaying)
			{
				return;
			}

			ITimelineTrack track;

			if (canParallel && clip is ITimelineParallelClip)
			{
				track = GetEmptyTrackByTime(currentTimeCursor);
			}
			else
			{
				canParallel = clip is ITimelineParallelClip;

				if (!IsEmptyByTime(currentTimeCursor))
				{
					++currentTimeCursor;
				}

				track = GetEmptyTrackByTime(currentTimeCursor);
			}

			track.InsertClip(currentTimeCursor, clip);
		}

		private bool IsEmptyByTime(int time)
		{
			foreach (var track in tracks)
			{
				if (!track.IsEmpty(time))
				{
					return false;
				}
			}

			return true;
		}

		private void Clear()
		{
			canParallel = false;
			isPlaying = false;
			currentTimeCursor = 0;

			foreach (var track in tracks)
			{
				track.ClearClips();
			}
		}
	}
}
