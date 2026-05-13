using System;
using System.Collections.Generic;

namespace Part2_CyberChatBot
{
    internal class Responses
    {
        // this is part of memory, so that the bot can remember things like the users name.
        public string UserName { get; set; }
        public string FavouriteTopic { get; set; }

        public string[] ConversationKeys;
        public string[] ConversationValues;

        private Dictionary<string, List<string>> randomResponses;
        private Random random = new Random();

        // these are the words that the bot will look for to detect the users feelings. (personalization)
        private string[] worriedWords = { "worried", "scared", "afraid", "nervous", "anxious" };
        private string[] curiousWords = { "curious", "interested", "wondering", "want to know" };
        private string[] frustratedWords = { "frustrated", "angry", "annoyed", "confused", "lost" };

        // this is for follow up statements, so that the bot can carry on with the conversation. (personalization)
        private string lastTopic = "";
        private string[] followUpPhrases = { "tell me more", "explain more", "give me another tip", "more info", "go on", "continue" };

        public void LoadResponses()
        {
            // the small talk responses are stored in parallel arrays.
            ConversationKeys = new string[]
            {
                "how are you",
                "what's your purpose",
                "what can i ask you about",
                "who made you",
                "what is your name",
                "thank you",
                "thanks",
                "hello",
                "hi",
                "good morning",
                "good afternoon",
                "good evening",
                "help"
            };

            ConversationValues = new string[]
            {
                "I am a bot so I have no emotions, but I am always ready to help you stay safe online!",
                "My purpose is to teach you about cybersecurity and help you stay safe on the internet.",
                "You can ask me about many things! Type 'topics' to see the full list.",
                "I was created by a student as part of a cybersecurity awareness project.",
                "My name is CyberBot! I am here to keep you safe online.",
                "You are welcome! Stay safe out there.",
                "No problem at all! Feel free to ask me anything else.",
                "Hello there! How can I help you stay safe online today?",
                "Hi! Ask me anything about cybersecurity. Type 'topics' to see what I know.",
                "Good morning! Ready to learn something about cybersecurity today?",
                "Good afternoon! What cybersecurity topic can I help you with?",
                "Good evening! Stay safe online. What would you like to know?",
                "Sure! Type 'topics' to see everything I can help you with."
            };

            // This is for the random responses, that are stored inside a dictionary, where the key is the topic, and the value is a list of responses for that topic.
            randomResponses = new Dictionary<string, List<string>>
            {
                {
                    "phishing", new List<string>
                    {
                        "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                        "Never click on suspicious links in emails, even if they look like they come from your bank.",
                        "Check the sender's email address carefully. Fake emails often have small spelling mistakes in the address.",
                        "If an email creates a sense of urgency like 'Act now!', that is a big red flag for phishing.",
                        "Legitimate companies will never ask for your password via email."
                    }
                },
                {
                    "password", new List<string>
                    {
                        "Use a strong password with at least 12 characters, mixing numbers, symbols, and capital letters.",
                        "Never use the same password for two different accounts. Use a password manager to stay safe.",
                        "Avoid using personal details like your birthday or pet's name as your password.",
                        "Change your passwords regularly, especially after a data breach.",
                        "A passphrase like 'Coffee!Sunsets2024' is both strong and easy to remember."
                    }
                },
                {
                    "scam", new List<string>
                    {
                        "If an offer sounds too good to be true, like winning a lottery you never entered, it is definitely a scam.",
                        "Never send money to someone you have never met in person.",
                        "Be careful of fake job offers that ask you to pay upfront fees.",
                        "Romance scams are real. Be cautious of online relationships that quickly ask for money.",
                        "Always verify before you trust. Call the organisation directly if you are unsure."
                    }
                },
                {
                    "privacy", new List<string>
                    {
                        "Review your social media privacy settings regularly to control who sees your posts.",
                        "Never share your ID number, home address, or phone number with strangers online.",
                        "Use a VPN to keep your browsing activity private, especially on public Wi-Fi.",
                        "Read the privacy policy of apps before installing them to know what data they collect.",
                        "Turn off location sharing for apps that do not really need it."
                    }
                }
            };
        }

        // the buttons that are close to the all topics button, this is for giving random responses when the user clicks on those buttons.
        public string GetRandomResponse(string topic)
        {
            foreach (var key in randomResponses.Keys)
            {
                if (topic.Contains(key))
                {
                    // remember the topic so follow up questions work
                    lastTopic = key;
                    var list = randomResponses[key];
                    return list[random.Next(list.Count)];
                }
            }
            return null;
        }

        public string HandleFollowUp(string userInput)
        {
            foreach (string phrase in followUpPhrases)
            {
                if (userInput.Contains(phrase))
                {
                    // if we remember a topic, give another tip on it
                    if (!string.IsNullOrEmpty(lastTopic))
                    {
                        var list = randomResponses[lastTopic];
                        return "Here is another tip on " + lastTopic + ": " + list[random.Next(list.Count)];
                    }
                    return "Could you tell me which topic you would like to know more about?";
                }
            }
            return null;
        }

        // sentiment detection, which is for the bot to see how the user is feeling.
        public string DetectSentiment(string userInput)
        {
            // Check if the user sounds worried
            foreach (string word in worriedWords)
            {
                if (userInput.Contains(word))
                {
                    return "It is completely understandable to feel that way. Cybersecurity can be overwhelming, but I am here to help! Let me share a tip: " + GetTipForWorried();
                }
            }

            // Checks if the user sounds curious
            foreach (string word in curiousWords)
            {
                if (userInput.Contains(word))
                {
                    return "I love the curiosity! Type 'topics' to explore everything I can teach you about cybersecurity.";
                }
            }

            // Checks if the user sounds frustrated
            foreach (string word in frustratedWords)
            {
                if (userInput.Contains(word))
                {
                    return "I am sorry you feel that way. Take a deep breath! Try typing 'topics' to see what I can help you with, and we will figure it out together.";
                }
            }

            return null; // no sentiment detected
        }

        // tips for when the user sounds worried, this is also for personalization.
        private string GetTipForWorried()
        {
            string[] tips =
            {
                "Always use strong, unique passwords for every account.",
                "Enable two-factor authentication wherever possible.",
                "Never click on links from unknown senders.",
                "Keep your software and antivirus updated at all times."
            };
            return tips[random.Next(tips.Length)];
        }

        public string GetConversationResponse(string lowerInput)
        {
            for (int i = 0; i < ConversationKeys.Length; i++)
            {
                if (lowerInput.Contains(ConversationKeys[i]))
                {
                    return ConversationValues[i];
                }
            }
            return null;
        }

        public string RememberFavouriteTopic(string userInput)
        {
            // looks for phrases like "i am interested in privacy"
            string[] interestPhrases = { "i am interested in", "i like", "i love", "i enjoy", "my favourite topic is" };
            foreach (string phrase in interestPhrases)
            {
                if (userInput.Contains(phrase))
                {
                    // grab whatever comes after the phrase
                    int index = userInput.IndexOf(phrase) + phrase.Length;
                    FavouriteTopic = userInput.Substring(index).Trim().TrimEnd('.');
                    return $"Great! I will remember that you are interested in {FavouriteTopic}. It is a crucial part of staying safe online! 🛡️";
                }
            }
            return null;
        }
    }
}

// references:
// Microsoft (n.d.) Dictionary<TKey, TValue> Class. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-10.0 (Accessed: 21 April 2026).
// Microsoft (n.d.) Random Class. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.random?view=net-10.0 (Accessed: 21 April 2026).
// Microsoft (n.d.) Random.Next Method. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.random.next?view=net-8.0 (Accessed: 21 April 2026).
