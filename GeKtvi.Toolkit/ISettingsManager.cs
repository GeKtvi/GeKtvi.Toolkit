using System;

namespace GeKtvi.Toolkit
{
    public interface ISettingsManager<SettingsType> : IDisposable
    {
        SettingsType Load();
        void Save();
        SettingsType? TryLoad();
    }
}