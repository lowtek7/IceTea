﻿using System.Collections;
using Core;

namespace Service
{
	/// <summary>
	/// 서비스를 구축하지 않았기 때문에 임시적으로 여기서
	/// </summary>
	public interface IGameService
	{
		void Init();
	}

	public interface IGameServiceCallback
	{
		/// <summary>
		/// 서비스가 주입 될 때 시점
		/// </summary>
		void OnActivate();

		/// <summary>
		/// 서비스가 주입 해제 될 때 시점
		/// </summary>
		void OnDeactivate();

		/// <summary>
		/// 모든 월드가 불러왔을때 콜백.
		/// 중간에 삽입된 서비스는 이것이 영원히 불릴 수 없다.
		/// </summary>
		void OnLoadWorld();

		/// <summary>
		/// IUpdate.Update 부르기 전에 무조건 한번 호출 되는 함수.
		/// </summary>
		void OnAwake();
	}

	/// <summary>
	/// 특정 서비스들은 로드방식일수도 있다... 로드를 기다릴 수 있는 코루틴이 존재하게 될 것이다.
	/// </summary>
	public interface ILoaderService
	{
		IEnumerator Load();
	}
}
