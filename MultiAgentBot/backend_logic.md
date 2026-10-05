# Backend Logic for TODO App

## Data Storage
- Simple JSON file to persist todo items.

Example JSON Structure:
```json
[
  {
    "id": 1,
    "description": "Sample Todo",
    "completed": false
  }
]
```

## CRUD Operations

1. **Create Todo**
   - Endpoint: `POST /todos`
   - Description: Adds a new todo item.
   - Request Body: `{ "description": "New Todo item" }`

2. **Read Todos**
   - Endpoint: `GET /todos`
   - Description: Retrieves all todo items.

3. **Update Todo**
   - Endpoint: `PUT /todos/:id`
   - Description: Updates a specific todo item.
   - Request Body: `{ "description": "Updated description", "completed": true/false }`

4. **Delete Todo**
   - Endpoint: `DELETE /todos/:id`
   - Description: Deletes a specific todo item.

## Integration
- Use Node.js and Express for server-side handling.
- Implement data save and retrieval functions to manipulate the JSON file.