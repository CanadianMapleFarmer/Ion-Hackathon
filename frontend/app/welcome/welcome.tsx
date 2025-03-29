import React, { useState, useRef, useEffect } from 'react';

// Define the type for a message
type Message = {
  type: 'user' | 'api';
  content: string;
};

export function Chat() {
  const [messages, setMessages] = useState<Message[]>([]); // Explicitly type messages as an array of Message objects
  const [input, setInput] = useState('');
  const textareaRef = useRef<HTMLTextAreaElement | null>(null);

  // Auto-resize the textarea as the user types.
  useEffect(() => {
    if (textareaRef.current) {
      textareaRef.current.style.height = 'auto';
      textareaRef.current.style.height = `${textareaRef.current.scrollHeight}px`;
    }
  }, [input]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!input.trim()) return;

    // Append the user's message.
    const newMessage: Message = { type: 'user', content: input };
    setMessages((prev) => [...prev, newMessage]);

    try {
      // Make an API call to fetch the response.
      const response = await fetch('https://localhost:7128/Chat', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ value: input }),
      });

      if (!response.ok) {
        throw new Error('Failed to fetch API response');
      }

      const data = await response.json();

      // Append the API's response.
      setMessages((prev) => [
        ...prev,
        { type: 'api', content: data.response }, // Assuming the API returns { response: string }
      ]);
    } catch (error) {
      console.error('Error fetching API response:', error);
      setMessages((prev) => [
        ...prev,
        { type: 'api', content: 'Error: Unable to fetch response from the API.' },
      ]);
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
              {msg.content}
            </div>
          </div>
        ))}
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
            className="absolute right-4 bottom-4 bg-sky-400 hover:bg-sky-600 text-white font-bold py-2 px-4 rounded">
            Send
          </button>
        </div>
      </form>
    </div>
  );
}