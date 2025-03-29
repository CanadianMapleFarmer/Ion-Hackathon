import React, { useState, useRef, useEffect } from 'react';
import ReactMarkdown from 'react-markdown'; // Import react-markdown

// Define the type for a message
type Message = {
  type: 'user' | 'api';
  content: string;
};

export function Chat() {
  const [messages, setMessages] = useState<Message[]>([]);
  const [input, setInput] = useState('');
  const [loading, setLoading] = useState(false);
  const [lastApiResponse, setLastApiResponse] = useState<string | null>(null); // Track the last API response
  const textareaRef = useRef<HTMLTextAreaElement | null>(null);

  const loadingMessages = [
    'Replacing the dev team...',
    'Generating actually good code...',
    'Thinking really hard...',
    'Automating your job...',
    'Making humans obsolete...',
    'Outperforming your team...',
    'Coding faster than you can...',
  ];
  const [loadingMessageIndex, setLoadingMessageIndex] = useState(0);

  // Auto-resize the textarea as the user types.
  useEffect(() => {
    if (textareaRef.current) {
      textareaRef.current.style.height = 'auto';
      textareaRef.current.style.height = `${textareaRef.current.scrollHeight}px`;
    }
  }, [input]);

  useEffect(() => {
    if (loading) {
      const interval = setInterval(() => {
        setLoadingMessageIndex((prevIndex) => (prevIndex + 1) % loadingMessages.length);
      }, 2000); // Change message every 2 seconds
      return () => clearInterval(interval);
    }
  }, [loading]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!input.trim() || loading) return;

    // Append the user's message.
    const newMessage: Message = { type: 'user', content: input };
    setMessages((prev) => [...prev, newMessage]);
    setLoading(true);

    // Clear the textarea.
    if (textareaRef.current) {
      textareaRef.current.style.height = 'auto';
      textareaRef.current.value = '';
    }
    setInput('');

    try {
      // Prepare the prompt with feedback loop if there's a previous API response.
      const prompt = lastApiResponse 
        ? `update this ${lastApiResponse} with ${input}` 
        : input;

      // Make an API call to fetch the response.
      const response = await fetch('https://localhost:7128/Chat', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ value: prompt }),
      });

      if (!response.ok) {
        throw new Error('Failed to fetch API response');
      }

      const data = await response.text(); // Treat response as plain text

      // Append the API's response and update the last API response.
      setMessages((prev) => [
        ...prev,
        { type: 'api', content: data }, // Use the plain text response
      ]);
      setLastApiResponse(data); // Store the last API response
    } catch (error) {
      console.error('Error fetching API response:', error);
      setMessages((prev) => [
        ...prev,
        { type: 'api', content: 'Error: Unable to fetch response from the API.' },
      ]);
    } finally {
      setLoading(false);
    }

    setInput('');
  };

  return (
    <div className="px-9 flex flex-col h-screen bg-gray-900 text-white">
      {/* Chat History */}
      <div className="flex-1 overflow-y-auto p-4 space-y-4">
        {messages.map((msg, index) => (
          <div key={index} className={`flex ${msg.type === 'user' ? 'justify-end' : 'justify-start'}`}>
            <div className={`max-w-md p-4 rounded-xl shadow ${msg.type === 'user' ? 'bg-sky-400' : 'bg-gray-700'}`}>
              {msg.type === 'api' ? (
                <ReactMarkdown>{msg.content}</ReactMarkdown> // Render Markdown for API responses
              ) : (
                msg.content
              )}
            </div>
          </div>
        ))}
        {loading && (
          <div className="flex items-center space-x-2 mt-2">
            <div className="spinner"></div> {/* Add spinner */}
            <div className="text-gray-400 italic text-lg">{/* Increase font size */}
              {loadingMessages[loadingMessageIndex]}
            </div>
          </div>
        )}
      </div>
      {/* Input Area */}
      <form onSubmit={handleSubmit} className="p-4">
        <div className="relative">
          <textarea
            ref={textareaRef}
            value={input}
            onChange={(e) => setInput(e.target.value)}
            placeholder="Type your message..."
            className="w-full p-4 bg-gray-800 text-white rounded-xl focus:outline-none resize-none overflow-hidden"
            rows={1}
          />
          <button 
            type="submit" 
            disabled={loading}
            className={`absolute right-4 bottom-4 bg-sky-400 hover:bg-sky-600 text-white font-bold py-2 px-4 rounded ${loading ? 'opacity-50 cursor-not-allowed' : ''}`}>
            Send
          </button>
        </div>
      </form>
    </div>
  );
}