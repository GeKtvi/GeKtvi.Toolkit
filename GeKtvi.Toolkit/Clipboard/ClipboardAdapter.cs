﻿using System;

namespace GeKtvi.Toolkit.Clipboard
{
    public class ClipboardAdapter : IClipboardAdapter
    {
        public Func<IDataObjectAdapter>? GetDataObjectFunc { get; init; }
        public Action<IDataObjectAdapter>? SetDataObjectAction { get; init; }
        public Func<object?>? GetTextAction { get; init; }

        public IDataObjectAdapter? GetDataObject() => GetDataObjectFunc?.Invoke();

        public void SetDataObject(IDataObjectAdapter dataObj) => SetDataObjectAction?.Invoke(dataObj);

        public object? GetText() => GetTextAction?.Invoke();
    }
}
