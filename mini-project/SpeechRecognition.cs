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
        public class SpeechCustom
        {
            private SpeechRecognitionEngine _recognizer;
            private SpeechSynthesizer Friday;
            private SpeechRecognitionEngine startlistening;
            private Form1 _form;

            public SpeechCustom(Form1 form) 
            { 
                _form = form;
                _recognizer = new SpeechRecognitionEngine();
                Friday = new SpeechSynthesizer();
                startlistening = new SpeechRecognitionEngine();
            }


            public void SettingUp()
            {
                _recognizer.SetInputToDefaultAudioDevice();
                _recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommands.txt")))));
                _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Default_SpeechRecognized);
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
                    _form.CompleteTask();
                    Friday.SpeakAsync(_form.lbl_task.Text);
                }
                if (speech == "Solve task")
                {
                    _form.Solve();
                }
                if (speech == "Use basic values")
                {
                    _form.BuiltinValues();
                }
            }

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
        }
    }
}
