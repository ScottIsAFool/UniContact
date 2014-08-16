using Cimbalino.Phone.Toolkit.Services;

namespace UniContact.Design
{
    public class EmptyApplicationSettingsService : IApplicationSettingsService
    {
        public T Get<T>(string key)
        {
            return default(T);
        }

        public T Get<T>(string key, T defaultValue)
        {
            return default(T);
        }

        public void Set<T>(string key, T value)
        {
        }

        public void Reset(string key)
        {
        }

        public void Save()
        {
        }

        public bool IsDirty { get; private set; }
    }
}
