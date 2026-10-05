# TODO App Architecture

## Components

1. **Frontend**: 
   - User Interface: HTML/CSS/JavaScript (Consider using a framework like React or Vue if necessary)
   - User Actions: Add, View, Edit, Delete, and Mark Todo items as completed.

2. **Backend**:
   - Data Handling: Simple JSON file or In-memory storage for saving todos.
   - API Endpoints: CRUD operations for managing todos.

3. **Integration**:
   - AJAX/Fetch API: For async communication between frontend and backend.

## Workflow

1. **Add Todo**: User inputs todo details -> Submit -> Add to the list and store in backend.
2. **View Todos**: Display list of todos with options to edit or delete.
3. **Edit Todo**: Modify existing todo details.
4. **Delete Todo**: Remove todo from the list and backend storage.
5. **Mark as Completed**: Toggle the completion status of a todo item.