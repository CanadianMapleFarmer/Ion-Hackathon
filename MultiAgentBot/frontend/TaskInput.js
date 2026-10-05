import React, { useState } from 'react';
import { View, TextInput, Button, StyleSheet } from 'react-native';

const TaskInput = ({ onAddTask }) => {
  const [taskName, setTaskName] = useState('');

  const handleAddTask = () => {
    onAddTask(taskName);
    setTaskName('');
  };

  return (
    <View style={styles.inputContainer}>
      <TextInput
        style={styles.input}
        placeholder="Add new task"
        value={taskName}
        onChangeText={setTaskName}
      />
      <Button title="Add" onPress={handleAddTask} />
    </View>
  );
};

const styles = StyleSheet.create({
  inputContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 10,
  },
  input: {
    width: '80%',
    borderColor: 'gray',
    borderWidth: 1,
    padding: 5,
  },
});

export default TaskInput;
