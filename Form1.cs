using System;
using System.Collections;
using System.IO;
using System.Speech.Recognition;
using System.Windows.Forms;


namespace AI{
    public partial class Form1 : Form{

        SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine();

        private const string Dictonary_Path = @"C:\Users\User\Desktop\wordsList.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ArrayList WordDictonary = new ArrayList();
 
                if (File.Exists(Dictonary_Path))
                {
                    using (StreamReader sr = File.OpenText(Dictonary_Path))
                    {
                        string word = "";
                        while ((word = sr.ReadLine()) != null)
                        {
                            WordDictonary.Add(word);
                        }
                    }
 
                }


                string[] wordsToCOMMANDS = (String[])WordDictonary.ToArray(typeof(string));
                

                Choices commands = new Choices(wordsToCOMMANDS);
                GrammarBuilder grBuilder = new GrammarBuilder();
                grBuilder.Append(commands);

                Grammar grammar = new Grammar(grBuilder);
                recognitionEngine.LoadGrammar(grammar);

                recognitionEngine.SetInputToDefaultAudioDevice();
                recognitionEngine.SpeechRecognized += speecEngine_SpeechRecognized;


            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        void speecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e){
            switch(e.Result.Text){
                case "hello":
                    richTextBox1.Text = "hello abdull";
                    break;
                case "print my name":
                   MessageBox.Show("Hello Abdull");
                    break;
                case "close":
                    Close();

                    break;
            }
            
        }
        private void btnEnable_Click(object sender, EventArgs e)
        {
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
            btnEnable.Enabled = false;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recognitionEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
            btnEnable.Enabled = true;
        }
    }
}
