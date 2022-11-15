﻿using System;
using System.Collections.Generic;
using System.Text;
using Uno.Disposables;
using Uno.UI.Helpers.WinUI;
using Windows.UI.Xaml.Controls;

namespace Microsoft.UI.Xaml.Controls;

[Flags]
internal enum  TextControlButtons
{
	None = 0x0000,
    Cut = 0x0001,
    Copy = 0x0002,
    Paste = 0x0004,
    Bold = 0x0008,
    Italic = 0x0010,
    Underline = 0x0020,
    Undo = 0x0040,
    Redo = 0x0080,
    SelectAll = 0x0100,
}

partial class TextCommandBarFlyout
{
	private Dictionary<TextControlButtons, ICommandBarElement> m_buttons;
	private AppBarButton m_proofingButton;

	private List<IDisposable> m_buttonCommandRevokers;
	private List<IDisposable> m_buttonClickRevokers;
	private List<IDisposable> m_toggleButtonCheckedRevokers;
	private List<IDisposable> m_toggleButtonUncheckedRevokers;

	private SerialDisposable m_proofingButtonLoadedRevoker = new();

	private List<IDisposable> m_proofingMenuItemClickRevokers;
	private List<IDisposable> m_proofingMenuToggleItemClickRevokers;
	private DispatcherHelper m_dispatcherHelper;

	private bool m_isSettingToggleButtonState = false;
}
