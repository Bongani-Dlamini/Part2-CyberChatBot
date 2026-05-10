using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Part2_CyberChatBot
{
    public partial class MainWindow : Window
    {
        // the role of these 3 classes is to separate the different needs of the program, that the user will need.
        private UIHelper graphics = new UIHelper();
        private CyberQuestions botBrain = new CyberQuestions();
        private Responses responses = new Responses();

        // stores the user's name after they type it
        private string userName = " ";

        // this is the constructor for the Mainwindow class, also know as our wpf window.
        public MainWindow()
        {
            InitializeComponent();

            // now for these 2 lines, the 2 classes i mentioned above, will be called to load the questions and responses.
            botBrain.LoadQuestions();
            responses.LoadResponses();


            AsciiArtBlock.Text = graphics.GetAsciiArt();

            // Play the voice greeting just like Part 1
            graphics.PlayVoiceGreeting();

            // Show a welcome message in the chat area
            graphics.AddBotMessage(txtDisplay,
                "Welcome to the Cybersecurity Awareness Bot! Please enter your name to get started.");
        }


        private void StartChat(object sender, RoutedEventArgs e)
        {
            string nameInput = txtName.Text.Trim();

            // Input validation, which is the same logic as Part 1
            if (string.IsNullOrWhiteSpace(nameInput))
            {
                graphics.AddBotMessage(txtDisplay, "Please enter a valid name to continue.");
                return;
            }

            // Save the name in both this class and Responses (for memory)
            userName = nameInput;
            responses.UserName = userName;

            // this line of code is for hiding the name input panel, so that the user cannot change their name after starting the chat.
            NamePanel.Visibility = Visibility.Collapsed;

            // Enable the chat input and send button
            txtUserInput.IsEnabled = true;
            btnSend.IsEnabled = true;
            txtUserInput.Focus();

            // Greet the user by their name.
            graphics.AddDivider(txtDisplay);
            graphics.AddBotMessage(txtDisplay, $"Hello, {userName} I am your cyberchatbot, here to help you stay safe online.");

            graphics.AddBotMessage(txtDisplay, "You can type 'topics' to see what I can help you with, or click one of the quick buttons below!");

            graphics.AddDivider(txtDisplay);
        }

        // pressing Enter in the name box also starts the chat
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                StartChat(sender, e);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void SendMessage()
        {
            string userInput = txtUserInput.Text.Trim();

            // Error handling — do not send empty messages
            if (string.IsNullOrWhiteSpace(userInput))
            {
                graphics.AddBotMessage(txtDisplay, "You didn't type anything! Please ask me a question.");
                return;
            }

            // Shows the user's message in the chat
            graphics.AddUserMessage(txtDisplay, userInput, userName);

            // Checks if the user has typed in "exit"
            if (userInput.ToLower() == "exit")
            {
                graphics.AddBotMessage(txtDisplay, $"Goodbye, {userName}! Stay safe when using the internet.");

                txtUserInput.IsEnabled = false;
                btnSend.IsEnabled = false;
                txtUserInput.Clear();
                return;
            }

            // Checks if the user has typed in "topics"
            if (userInput.ToLower() == "topics")
            {
                string topics = botBrain.GetAllTopics();
                graphics.AddDivider(txtDisplay);
                graphics.AddBotMessage(txtDisplay, topics);
                graphics.AddDivider(txtDisplay);
                txtUserInput.Clear();
                return;
            }

            // in very short words, what this does is that it checks the users favourite topic, so that the bot can give a more personalized reply.
            string replyPrefix = " ";
            if (!string.IsNullOrEmpty(responses.FavouriteTopic))
            {
                if (userInput.ToLower().Contains(responses.FavouriteTopic.ToLower()))
                {
                    replyPrefix = $"As someone interested in {responses.FavouriteTopic}, here is something useful: ";
                }
            }

            string botReply = botBrain.GetAnswerForUser(userInput, responses);

            graphics.AddDivider(txtDisplay);
            graphics.AddBotMessage(txtDisplay, replyPrefix + botReply);
            graphics.AddDivider(txtDisplay);

            // Clears the input box
            txtUserInput.Clear();
        }
        // what this method is for, is if the user clicks one of the quick topic buttons, which is next to the all topics buttons.
        private void QuickTopic_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userName)) return;

            var button = sender as System.Windows.Controls.Button;
            if (button == null) return;

            // put the button's tag into the input box and send it
            txtUserInput.Text = button.Tag.ToString();
            SendMessage();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Document.Blocks.Clear();
            graphics.AddBotMessage(txtDisplay,
                "Chat cleared! What would you like to know, " +
                (string.IsNullOrEmpty(userName) ? "friend" : userName) + "?");
        }
    }
}
