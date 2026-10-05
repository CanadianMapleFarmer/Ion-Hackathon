import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';

const TaskItem = ({ task, onRemove, onToggle }) => {
  return (
    <TouchableOpacity onPress={() => onToggle(task.id)}>
      <View style={styles.taskItem}>
        <Text style={task.completed ? styles.completed : styles.uncompleted}>{task.name}</Text>
        <Button title="Delete" onPress={() => onRemove(task.id)} />
      </View>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  taskItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 10,
    marginVertical: 5,
    backgroundColor: '#f9f9f9',
    borderRadius: 5,
  },
  completed: {
    textDecorationLine: 'line-through',
    color: 'gray',
  },
  uncompleted: {
    color: 'black',
  },
});

export default TaskItem;
