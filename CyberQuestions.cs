using System;
using System.Collections.Generic;

namespace Part2_CyberChatBot
{
    internal class CyberQuestions
    {
        // Parallel arrays to store keywords and their answers
        // Same structure as Part 1
        public string[] Questions;
        public string[] Answers;

        // loads all questions and answers into the arrays.
        public void LoadQuestions()
        {
            Questions = new string[]
            {
                "antivirus",
                "ransomware",
                "spyware",
                "keyloggers",
                "trojan horse",
                "viruses",
                "malware",
                "phishing emails",
                "phishing",
                "two-factor auth",
                "strong passwords",
                "passwords",
                "firewalls",
                "vpn",
                "social engineering",
                "identity theft",
                "encryption",
                "data breaches",
                "cyberbullying",
                "https",
                "public wi-fi",
                "ddos attacks",
                "personal information",
                "cookies",
                "shoulder surfing",
                "brute force",
                "data backups",
                "what to do if hacked",
                "online scams",
                "software updates"
            };

            Answers = new string[]
            {
                "Antivirus software is a program that scans, detects, and removes viruses and other malware from your computer.",
                "Ransomware is a type of malware that locks your files until you pay a 'ransom' to the hacker.",
                "Spyware is software that secretly records what you do on your computer, like stealing your passwords.",
                "Keyloggers are tools that record every single key you press on your keyboard to steal login details.",
                "A Trojan is a malicious program disguised as something useful (like a free game) to trick you into installing it.",
                "Computer viruses are programs that copy themselves and spread to other files to damage your system.",
                "Malware is a general term for any 'Malicious Software' designed to harm or exploit your device.",
                "These are fake emails that look like they are from your bank or Netflix, but they want to steal your password.",
                "Phishing is a scam where criminals trick you into giving them your private information.",
                "2FA (or MFA) is when you need a second step to log in, like a code sent to your cell phone.",
                "A strong password has at least 12 characters, including numbers, symbols, and capital letters.",
                "Never use the same password for two different accounts! Use a password manager to stay safe.",
                "A firewall acts like a digital wall that monitors network traffic to block unauthorized access.",
                "A Virtual Private Network (VPN) encrypts your internet connection to keep your location and data private.",
                "This is when hackers use human psychology to trick you into revealing secrets, like pretending to be tech support.",
                "This is when someone steals your personal info (ID number, name) to open bank accounts or take loans in your name.",
                "Encryption turns your data into a secret code so that only people with the right key can read it.",
                "A data breach happens when a company's database is hacked and customer information is leaked online.",
                "Cyberbullying is using the internet or social media to harass, threaten, or shame another person.",
                "The 'S' in HTTPS stands for 'Secure'. It means your connection to that website is private and encrypted.",
                "Never log into your bank or private accounts on public Wi-Fi as hackers can 'sniff' your data.",
                "A DDoS attack is when hackers flood a website with so much fake traffic that the site crashes.",
                "Never share your ID number, home address, or phone number with strangers online.",
                "Cookies are small files websites save on your computer to remember your settings or track your browsing.",
                "This is when someone looks over your shoulder while you type your PIN or password at an ATM or laptop.",
                "A brute force attack is when a hacker uses a computer to guess thousands of password combinations.",
                "Always keep a copy of your important files on an external drive or cloud service in case your computer breaks.",
                "Disconnect from the internet, run an antivirus scan, and change all your passwords immediately.",
                "If an offer sounds too good to be true, like winning a lottery you never entered, it is definitely a scam.",
                "Updates fix security holes. Always click 'Update' on your phone and laptop to stay protected."
            };
        }

        public string GetAnswerForUser(string userInput, Responses responses)
        {
            string lowerInput = userInput.ToLower();

            // checks for follow up questions, so that the bot can continue the conversation with the user. (personalization.)
            string followUp = responses.HandleFollowUp(lowerInput);
            if (followUp != null)
                return followUp;

            // in short this is for the bot to help the user with their feelings. wich is also personalization.
            string sentiment = responses.DetectSentiment(lowerInput);
            if (sentiment != null)
                return sentiment;

            string memory = responses.RememberFavouriteTopic(lowerInput);
            if (memory != null)
                return memory;

            // thois code below is the bot holding the conversation with the user.(personlization.)
            string conversation = responses.GetConversationResponse(lowerInput);
            if (conversation != null)
                return conversation;

            string randomReply = responses.GetRandomResponse(lowerInput);
            if (randomReply != null)
                return randomReply;

            // what this code is for is to check if the user input contains any of the keywords in the Questions array, so that it can give proper answers.
            for (int i = 0; i < Questions.Length; i++)
            {
                if (lowerInput.Contains(Questions[i]))
                    return Answers[i];
            }

            // this is for error handling, so that the app doesnt crash even though the user types in something wrong.
            return "I'm not sure I understand. Can you try rephrasing? Type 'topics' to see what I can help you with!";
        }
        // this is the line of code that helps the user see all the topics the bot can talk about.
        public string GetAllTopics()
        {
            string result = "Here are the topics you can ask me about:  ";
            for (int i = 0; i < Questions.Length; i++)
            {
                result += $"  • {Questions[i]}";
            }
            result += "Which topic would you like to know more or talk about about?";
            return result;
        }
    }
}

// References:
// Microsoft (n.d.) C# Arrays. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays (Accessed: 23 April 2026).
// Microsoft (n.d.) String.Contains Method. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0 (Accessed: 23 April 2026).
// Microsoft (n.d.) String.ToLower Method. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.string.tolower?view=net-10.0 (Accessed: 23 April 2026).