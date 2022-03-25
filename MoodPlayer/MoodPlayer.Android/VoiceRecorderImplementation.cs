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

        public int sampleRate = 44100;
        public int ArrayLength = 100000;
        public ChannelIn channelIn = ChannelIn.Mono;
        public AudioSource audioSource = AudioSource.Mic;
        public Encoding encoding = Encoding.Pcm16bit;

        byte[] audioBuffer;

        AudioRecord audioRecorder;
        
        [MTAThread]
        public void Record()
        {
            audioBuffer = new byte[ArrayLength];

            audioRecorder = new AudioRecord(audioSource, sampleRate, channelIn, encoding, ArrayLength);


            if (!IsRecording)
            {

                audioRecorder.StartRecording();
                ulong sequence_no = 0;
                Task.Run(() =>
                {
                    while (true)
                    {
                        audioRecorder.Read(audioBuffer, 0, ArrayLength);
                        VoiceController.CurrentReadingRecord.DateTime = DateTime.Now;
                        VoiceController.CurrentReadingRecord.ChannelType = channelIn.ToString();
                        VoiceController.CurrentReadingRecord.Encoding = encoding.ToString();
                        VoiceController.CurrentReadingRecord.SequenceId = sequence_no;
                        VoiceController.CurrentReadingRecord.Length = ArrayLength;
                        VoiceController.CurrentReadingRecord.SampleRate = sampleRate;
                        VoiceController.CurrentReadingRecord.Data = audioBuffer;
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