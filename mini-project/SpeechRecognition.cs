using System;
using System.Speech;
using System.IO;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;


namespace mini_project
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer Friday = new SpeechSynthesizer();
        SpeechRecognitionEngine startlistening = new SpeechRecognitionEngine();
        //int RecTimeOut = 0;

        public Form1()
        {
            InitializeComponent();
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommands.txt")))));
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Default_SpeechRecognized);
          //  _recognizer.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(_recognizer_SpeechRecognized);
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);

            startlistening.SetInputToDefaultAudioDevice();
            startlistening.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommands.txt")))));
            startlistening.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(startlistening_SpeechRecognized);
        }

        private void Default_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            if (speech == "Hello")
            {
                label1.Text = "Hi";
                Friday.SpeakAsync("Hello.");
            }
            if (speech == "Tell me about yourself")
            {
                Friday.SpeakAsync("I'm Friday - speech assistant for this program, you can ask me to do simple tasks.");
            }
            if (speech == "Stop talking")
            {
                Friday.SpeakAsyncCancelAll();
                Friday.SpeakAsync("Okay.");
            }
            if (speech == "Stop listening")
            {
                Friday.SpeakAsync("If you need me just call me.");
               _recognizer.RecognizeAsyncCancel();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
            }
            if (speech == "Complete task")
            {
                CompleteTask();
                Friday.SpeakAsync(lbl_task.Text);
            }
            if (speech == "Solve task")
            {
                Solve();
            }
            if (speech == "Use basic values")
            {
                BuiltinValues();
            }
        }


        //private void _recognizer_SpeechRecognized(object sender, SpeechDetectedEventArgs e)
        //{
        //    RecTimeOut = 0;
        //}

        private void startlistening_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;

            if (speech == "Friday")
            {
                startlistening.RecognizeAsyncCancel();
                Friday.SpeakAsync("I am ready to comply.");
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        //private void tmr_speaking_Tick(object sender, EventArgs e)
        //{
        //    if (RecTimeOut == 10)
        //    {
        //        _recognizer.RecognizeAsyncCancel();
        //    }
        //    else if (RecTimeOut == 11)
        //    {
        //        tmr_speaking.Stop();
        //        startlistening.RecognizeAsync(RecognizeMode.Multiple);
        //        RecTimeOut = 0;
        //    }
        //}
    }


}
