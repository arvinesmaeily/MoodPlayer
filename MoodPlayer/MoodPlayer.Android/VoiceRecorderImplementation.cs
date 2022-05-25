using Android.Media;
using DataCollectionManager.DependencyServices;
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
        public bool IsRecording = false;

        public const int sampleRate = 44100;
        public const int ArrayLength = 100000;
        public const ChannelIn channelIn = ChannelIn.Mono;
        public const AudioSource audioSource = AudioSource.Mic;
        public const Encoding encoding = Encoding.Pcm16bit;

        byte[] audioBuffer = new byte[ArrayLength];

        AudioRecord audioRecorder = new AudioRecord(audioSource, sampleRate, channelIn, encoding, ArrayLength);
        
        [MTAThread]
        public void Record()
        {

            if (!IsRecording)
            {
                audioRecorder.StartRecording();

                ulong sequence_no = 0;
                Task.Run(() =>
                {
                    while (true)
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
                });
            }

        }



        [MTAThread]
        public void Stop()
        {
            Task.Run(() =>
            {
                if (IsRecording)
                {
                    
                }
            });
        }
    }
}