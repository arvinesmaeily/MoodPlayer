using System;

namespace DependencyManager
{
    public interface IVoiceRecorder
    {
        void Initialize(string initialRecordPath, TimeSpan timeout);
        void TurnOn();
        void TurnOff();
    }
}
