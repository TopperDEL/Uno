﻿#nullable enable

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Uno.Disposables;
using Uno.UI.Xaml;

#if HAS_UNO_WINUI
using Windows.UI;
#endif

namespace Windows.UI.Xaml.Media
{
	[TypeConverter(typeof(BrushConverter))]
	public partial class Brush : DependencyObject
	{
		internal event Action? InvalidateRender;

		protected Brush()
		{
			InitializeBinder();
		}

#if __ANDROID__ || __IOS__ || __MACOS__
		internal static Color GetFallbackColor(Brush brush)
		{
			return brush switch
			{
				SolidColorBrush scb => scb.ColorWithOpacity,
				GradientBrush gb => gb.FallbackColorWithOpacity,
				XamlCompositionBrushBase xamlCompositionBrushBase => xamlCompositionBrushBase.FallbackColorWithOpacity,
				_ => SolidColorBrushHelper.Transparent.Color,
			};
		}
#endif

		public static implicit operator Brush(Color uiColor) => new SolidColorBrush(uiColor);

		public static implicit operator Brush(string colorCode) => SolidColorBrushHelper.Parse(colorCode);

		private protected void OnInvalidateRender() => InvalidateRender?.Invoke();

		internal virtual void OnPropertyChanged2(DependencyPropertyChangedEventArgs args)
		{
			if (args.Property == DataContextProperty || args.Property == TemplatedParentProperty)
			{
				return;
			}

			OnInvalidateRender();

			if (args.Property == TransformProperty || args.Property == RelativeTransformProperty)
			{
				if (args.NewValue is Transform newTransform)
				{
					newTransform.Changed += OnTransformChange;
				}

				if (args.OldValue is Transform oldTransform)
				{
					oldTransform.Changed -= OnTransformChange;
				}
			}
		}

		private void OnTransformChange(object? sender, EventArgs args) => OnInvalidateRender();

		#region Opacity Dependency Property

		public double Opacity
		{
			get => GetOpacityValue();
			set => SetOpacityValue(value);
		}

		[GeneratedDependencyProperty(DefaultValue = 1d, ChangedCallback = true)]
		public static DependencyProperty OpacityProperty { get; } = CreateOpacityProperty();

		protected virtual void OnOpacityChanged(double oldValue, double newValue)
		{
		}

		#endregion

		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "IS_UNIT_TESTS", "__WASM__", "__NETSTD_REFERENCE__", "__MACOS__")]
		[GeneratedDependencyProperty(DefaultValue = null)]
		public static DependencyProperty TransformProperty { get; } = CreateTransformProperty();

		[global::Uno.NotImplemented("__ANDROID__", "__IOS__", "IS_UNIT_TESTS", "__WASM__", "__NETSTD_REFERENCE__", "__MACOS__")]
		public Windows.UI.Xaml.Media.Transform Transform
		{
			get => GetTransformValue();
			set => SetTransformValue(value);
		}

		public Transform RelativeTransform
		{
			get => GetRelativeTransformValue();
			set => SetRelativeTransformValue(value);
		}

		[GeneratedDependencyProperty(DefaultValue = null, ChangedCallback = true)]
		public static DependencyProperty RelativeTransformProperty { get; } = CreateRelativeTransformProperty();

		protected virtual void OnRelativeTransformChanged(Transform oldValue, Transform newValue)
		{
		}

		private protected Color GetColorWithOpacity(Color referenceColor)
		{
			return Color.FromArgb((byte)(Opacity * referenceColor.A), referenceColor.R, referenceColor.G, referenceColor.B);
		}

		[Pure]
		internal static Color? GetColorWithOpacity(Brush? brush, Color? defaultColor = null)
		{
			return TryGetColorWithOpacity(brush, out var c) ? c : defaultColor;
		}

		[Pure]
		internal static bool TryGetColorWithOpacity(Brush? brush, out Color color)
		{
			switch (brush)
			{
				case SolidColorBrush scb:
					color = scb.ColorWithOpacity;
					return true;
				case GradientBrush gb:
					color = gb.FallbackColorWithOpacity;
					return true;
				case XamlCompositionBrushBase ab:
					color = ab.FallbackColorWithOpacity;
					return true;
				default:
					color = default;
					return false;
			}
		}
	}
}
