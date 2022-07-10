using Android.Media;
using DependencyManager;
using DataCollectionManager.Voice.VoiceDataUtils;
using DataCollectionManager.Voice.VoiceUtils;
using MoodPlayer.Droid;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(VoiceRecorderImplementation))]
namespace MoodPlayer.Droid
{
    public class VoiceRecorderImplementation : IVoiceRecorder
    {
        public void Start()
        {
            
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}


/*
        public const int sampleRate = 44100;
        public const int ArrayLength = 1000;
        public const ChannelIn channelIn = ChannelIn.Mono;
        public const AudioSource audioSource = AudioSource.Default;
        public const Encoding encoding = Encoding.Pcm16bit;

        byte[] audioBuffer = new byte [ArrayLength];

        AudioRecord audioRecorder = new AudioRecord(audioSource, sampleRate, channelIn, encoding, ArrayLength);

        public bool IsRecording = false;

        [MTAThread]
        private void Record()
        {

                    audioRecorder.StartRecording();
                    ulong sequence_no = 0;
                    while (true)
                    {
                        if (!IsRecording)
                        {
                            audioRecorder.Stop();
                            break;
                        }
                        else
                        {
                            
                            audioRecorder.Read(audioBuffer, 0, ArrayLength);

                            VoiceController.CurrentReadingRecord.DateTime = DataCollectionManager.MasterDataManager.DataRecordingManager.DateTimeToString(DateTime.Now);
                            VoiceController.CurrentReadingRecord.ChannelType = channelIn.ToString();
                            VoiceController.CurrentReadingRecord.Encoding = encoding.ToString();
                            VoiceController.CurrentReadingRecord.SequenceId = sequence_no.ToString();
                            VoiceController.CurrentReadingRecord.Length = ArrayLength;
                            VoiceController.CurrentReadingRecord.SampleRate = sampleRate;
                            VoiceController.CurrentReadingRecord.Data = System.Text.Encoding.Default.GetString(audioBuffer);

                            sequence_no++;
                        }

                    }

        }

        [MTAThread]
        public void Start()
        {
            Task.Run(() =>
            {
                if (!IsRecording)
                {
                    IsRecording = true;
                    Record();
                }
            });
        }

        [MTAThread]
        public void Stop()
        {
            Task.Run(() =>
            {
                if (IsRecording)
                {
                    IsRecording = false;
                }
            });
        }*/