using Android.Media;
using DependencyManager;
using DataCollectionManager.Voice.VoiceDataUtils;
using DataCollectionManager.Voice.VoiceUtils;
using MoodPlayer.Droid;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using DataCollectionManager.Voice.Models;
using System.IO;
using DataCollectionManager.MasterDataManager;

[assembly: Dependency(typeof(VoiceRecorderImplementation))]
namespace MoodPlayer.Droid
{
   public class VoiceRecorderImplementation : IVoiceRecorder
    {
        private bool Trigger;

        private bool Power;

        private bool IsRecording;

        private Thread LPTT;

        private long SequenceId;

        private DateTime DateTime;

        private TimeSpan Timeout;
        
        private string InitialRecordPath;
        
        private string RecordPath;

        private MediaRecorder Recorder;


        public void Initialize(string initialRecordPath, TimeSpan timeout)
        {
            

            Trigger = true;

            Power = false;

            IsRecording = false;
            
            LPTT = new Thread(new ThreadStart(PTThread));

            LPTT.SetApartmentState(ApartmentState.MTA);
            LPTT.IsBackground = false;
            LPTT.Priority = ThreadPriority.Highest;
            LPTT.Name = "LPTT";

            SequenceId = 0;

            DateTime = DateTime.Now;

            Timeout = timeout;
            
            RecordPath = "";
            
            InitialRecordPath = initialRecordPath;

            Recorder = new MediaRecorder();
        }
        
        public void TurnOn()
        {
            Power = true;
            LPTT.Start();
        }

        public void TurnOff()
        {
            Power = false;
        }
        private void Record()
        {
            if (!IsRecording)
            {

                Recorder.SetAudioSource(AudioSource.Default);
                Recorder.SetOutputFormat(OutputFormat.AacAdts);
                Recorder.SetAudioEncoder(AudioEncoder.Aac);
                
                Recorder.SetAudioChannels(1);
                Recorder.SetAudioEncodingBitRate(96000);
                Recorder.SetAudioSamplingRate(48000);
                
                DateTime = DateTime.Now;
                
                var dateTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}:{6}",
                DateTime.Year.ToString(), DateTime.Month.ToString(), DateTime.Day.ToString(), DateTime.Hour.ToString(),
                DateTime.Minute.ToString(), DateTime.Second.ToString(), DateTime.Millisecond.ToString());

                RecordPath = InitialRecordPath + SequenceId + "-" + dateTime + ".aac";

                Recorder.SetOutputFile(RecordPath);
                Recorder.Prepare();
                Recorder.Start();

                Debug.WriteLine("Current File: " + RecordPath);
                IsRecording = true;

            }
        }

        private void Stop()
        {
            if (IsRecording)
            {


                Recorder.Stop();

                var dateTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}:{6}",
                    DateTime.Year.ToString(), DateTime.Month.ToString(), DateTime.Day.ToString(), DateTime.Hour.ToString(),
                    DateTime.Minute.ToString(), DateTime.Second.ToString(), DateTime.Millisecond.ToString());

                var record = new VoiceRecord()
                {
                    DateTime = dateTime,
                    Path = RecordPath,
                    SequenceId = SequenceId.ToString()
                };
                VoiceController.CurrentReadingRecord = record;
                VoiceRecordManager.Records.Enqueue(record.ShallowCopy());
                SequenceId++;

                Recorder.Reset();
                IsRecording = false;
                Debug.WriteLine("Save File Event: " + VoiceController.CurrentReadingRecord.Path);

            }
        }

    [MTAThread]
        private void PTThread()
        {
                Device.StartTimer(TimeSpan.FromMilliseconds(500), RecordingThread);
        }

        [MTAThread]
        public bool RecordingThread()
        {
            if (Power)
            {
                if (Trigger && DateTime.Now.Second % Timeout.Seconds == 0)
                {
                    Trigger = false;
                    Stop();
                    Record();
                }
                else
                {
                    Trigger = true;
                }
            }
            return true;
        }
    }
}



