using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Part2_CyberChatBot
{
    internal class UIHelper
    {
        // this is the voice greeting that the user will hear as soon as they open the app, just like in Part 1.
        public void PlayVoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Greeting.wav");
                player.Play();
            }
            catch (Exception)
            {
                // if the file is missing, the app still runs fine
                Console.WriteLine("Audio file 'Greeting.wav' not found.");
            }
        }

        // used a rich text box for the chat display.
        public void AddBotMessage(RichTextBox chatBox, string message)
        {

            Paragraph para = new Paragraph();

            Run label = new Run("CyberBot: ")
            {
                Foreground = new SolidColorBrush(Colors.Cyan),
                FontWeight = System.Windows.FontWeights.Bold
            };

            Run text = new Run(message)
            {
                Foreground = Brushes.White
            };

            para.Inlines.Add(label);
            para.Inlines.Add(text);
            para.Margin = new Thickness(4);

            chatBox.Document.Blocks.Add(para);

            // what this does is that it automatically scrolls to the end, so that the user can see the latest message.
            chatBox.ScrollToEnd();
        }

        // ═══════════════════════════════════════════════════════
        // ADD USER MESSAGE: adds the user's message to the chat
        // ═══════════════════════════════════════════════════════
        public void AddUserMessage(RichTextBox chatBox, string message, string userName)
        {
            Paragraph para = new Paragraph();

            Run label = new Run($"You - {userName}: ")
            {
                Foreground = new SolidColorBrush(Colors.Gold),
                FontWeight = System.Windows.FontWeights.Bold
            };

            Run text = new Run(message)
            {
                Foreground = Brushes.LightGray
            };

            para.Inlines.Add(label);
            para.Inlines.Add(text);
            para.Margin = new System.Windows.Thickness(4);

            chatBox.Document.Blocks.Add(para);
            chatBox.ScrollToEnd();
        }
        // here i am adding a divider to separate the different sections of the chat.
        public void AddDivider(RichTextBox chatBox)
        {
            Paragraph para = new Paragraph(new Run("──────────────────────────────────────────"))
            {
                Foreground = new SolidColorBrush(Colors.DarkBlue),
                Margin = new System.Windows.Thickness(2)
            };
            chatBox.Document.Blocks.Add(para);
        }

        // this is the ASCII image that will be shown by the header of the app.
        public string GetAsciiArt()
        {
            return
                "  ____      _               ____ _           _   ____        _   \n" +
                " / ___|___ | |__   ___ _ __/ ___| |__   __ _| |_| __ )  ___ | |_ \n" +
                "| |   / _ \\| '_ \\ / _ \\ '__| |   | '_ \\ / _` | __|  _ \\ / _ \\| __|\n" +
                "| |__| (_) | |_) |  __/ |  | |___| | | | (_| | |_| |_) | (_) | |_ \n" +
                " \\____\\___/|_.__/ \\___|_|   \\____|_| |_|\\__,_|\\__|____/ \\___/ \\__|";
        }
    }
}
