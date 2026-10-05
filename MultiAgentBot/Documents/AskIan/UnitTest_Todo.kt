import org.junit.Test
import kotlin.test.assertEquals
import kotlin.test.assertFalse
import kotlin.test.assertTrue

class TodoManagerUnitTest {

    private val todoManager = TodoManager()

    @Test
    fun `test add todo`() {
        todoManager.addTodo("Learn Kotlin")
        val todos = todoManager.getTodos()
        assertEquals(1, todos.size)
        assertEquals("Learn Kotlin", todos[0].description)
    }

    @Test
    fun `test edit todo`() {
        todoManager.addTodo("Learn Kotlin")
        todoManager.editTodo(1, "Learn advanced Kotlin")
        val todos = todoManager.getTodos()
        assertEquals("Learn advanced Kotlin", todos[0].description)
    }

    @Test
    fun `test delete todo`() {
        todoManager.addTodo("Learn Kotlin")
        todoManager.deleteTodo(1)
        val todos = todoManager.getTodos()
        assertTrue(todos.isEmpty())
    }

    @Test
    fun `test toggle complete`() {
        todoManager.addTodo("Learn Kotlin")
        todoManager.toggleComplete(1)
        assertTrue(todoManager.getTodos()[0].isCompleted)
        todoManager.toggleComplete(1)
        assertFalse(todoManager.getTodos()[0].isCompleted)
    }
}