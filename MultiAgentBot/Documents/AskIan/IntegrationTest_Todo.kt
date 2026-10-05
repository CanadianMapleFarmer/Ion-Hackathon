import org.junit.Test
import kotlin.test.assertEquals

class TodoIntegrationTest {

    private val todoManager = TodoManager()

    @Test
    fun `test full workflow`() {
        // Add todos
        todoManager.addTodo("Learn Kotlin")
        todoManager.addTodo("Go Shopping")

        // Check todos
        var todos = todoManager.getTodos()
        assertEquals(2, todos.size)

        // Edit and check
        todoManager.editTodo(1, "Learn advanced Kotlin")
        assertEquals("Learn advanced Kotlin", todos[0].description)

        // Toggle completion and check
        todoManager.toggleComplete(2)
        assertTrue(todos[1].isCompleted)

        // Delete and check
        todoManager.deleteTodo(1)
        todos = todoManager.getTodos()
        assertEquals(1, todos.size)
    }
}