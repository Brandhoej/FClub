﻿namespace FClub.UI.Scene
{
	public interface ISceneNavigationHandler<TSceneInput> where TSceneInput : ISceneInput
	{
		IMenuComponent<TSceneInput> Focused { get; }

		void Addnavigation(IMenuComponent<TSceneInput> from, SceneNavigationDirection direction, IMenuComponent<TSceneInput> to);
		void SetFocus(IMenuComponent<TSceneInput> menuComponent);
		void Move(SceneNavigationDirection direction);
	}
}
