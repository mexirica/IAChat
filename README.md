# IAChat

**IAChat** is a console application that allows you to interact with an AI model by sending questions and receiving answers in real-time. The program uses a chat client (such as **OllamaChatClient**) to communicate with the AI and dynamically display the responses.

## Features

- **Real-time Interaction with AI**: The application sends questions to the AI model and displays responses as they are generated.
- **Canceling Responses**: The user can press the `Esc` key to interrupt the ongoing response process and return to the next question.
- **Conversation History Storage**: Interactions are saved and used as context for the AI, helping to improve the quality of responses.

## How to Use

### Prerequisites

Before using **IAChat**, make sure you have the following prerequisites:

1. **.NET 9.0 or higher** installed on your machine.
2. An accessible AI model endpoint that can be reached via a URL (such as **OllamaChatClient**).

### Installing and Setting Up the Project

1. Clone the repository to your local directory:

   ```bash
   git clone https://github.com/your-username/IAChat.git
   cd IAChat
    ```

2. Restore the packages:

   ```bash
   dotnet restore
   ```

3. Build and Execute:

    ```bash
    dotnet build --configuration Release
    ./bin/Release/net9.0/IAChat -c <chat-url> -m <model-name>
    ```

4. Just Ask!
   ```bash
    Ask your question: Tell me about Italy
   ```

5. If you wanna to install it in your terminal, just execute:

   ```bash
   dotnet tool install --global --add-source ./bin/Release iachat
   ```                   



