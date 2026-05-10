# CyberChatBot for Part 2
## Features of the chatbot for Part 2 of the POE:
1. GUI Interface — Built with WPF, dark grey theme, clean and user-friendly layout
2. Voice Greeting — Plays an audio greeting when the app starts
3. ASCII Art — Displays the CyberBot logo at the top of the window
4. Keyword Recognition — Recognises 30+ cybersecurity keywords and provides relevant responses
5. Random Responses — Randomly selects from multiple responses for topics like phishing, passwords, scams and privacy
6. Conversation Flow — Handles follow-up phrases like "tell me more" or "give me another tip"
7. Memory and Recall — Remembers the user's name and favourite topic to personalise responses
8. Sentiment Detection — Detects when the user feels worried, curious or frustrated and responds with empathy
9. Error Handling — Friendly fallback message for unrecognised inputs, no crashes

## What was used to make this application:
When you open Visual Studio, which is the IDE we are using this year, you will select a template called WPF application, which you will then code in C#.

## How to use the application:
1. The user must type in their name and then press the start chat button.
2. The user must then type in a question and press enter or even click the send button.
3. The user can also use the quick buttons for topics like, Passwords, Phishing, Privacy, Scams, Malware, All Topics, which are located next to the buttons at the bottom.
4. If the user is having problems of cant get an answer that they want they can type  topics to see the full list of things the bot can help with.
5. The user can press the clear button to wipe the chat display.
6. Then finally the user can type in exit to end the chat or exit the program.

## Project Structure(classes):
- MainWindow.xaml: This is bassically the class where the i designed how the app must look like.
- MainWindow.xaml.cs: This is for coding what i designed and also calling the other 3 classes that i have.
- CyberQuestions.cs: This is where the questions regardig cyber security are handled.
- Responses.cs: This is the class that deals with the user intercation, between the bot and the user.
- UIHelper.cs: This is the class that handles the ascii image, the voice greeting and much more.

## How to run the App:
The user can press F5 on their own laptops or PC's or at the very top on Visual Studio(the IDE i am using), there are 2 green play buttons, the user can click either one the app will run.
