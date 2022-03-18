using Android.Media;
using DataCollectionManager.DependencyServices;
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
        [Obsolete]
        public async void Record()
        {
            audioBuffer = new byte[ArrayLength];

            audioRecorder = new AudioRecord(audioSource, sampleRate, channelIn, encoding, ArrayLength);

            await Task.Run(() =>
            {
                if (!IsRecording)
                {   
                    
                    audioRecorder.StartRecording();
                    while (true)
                    {

                        audioRecorder.Read(audioBuffer, 0, ArrayLength);

                    }
                }
            });

        }



        [MTAThread]
        public async void Stop()
        {
            await Task.Run(() =>
            {
                if (IsRecording)
                {
                    
                }
            });
        }
    }
}