import React from 'react';
import { View, Text, StyleSheet } from 'react-native';

const TaskList = ({ tasks }) => {
  return (
    <View style={styles.container}>
      {tasks.map((task) => (
        <Text key={task.id}>{task.name}</Text>
      ))}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
  },
});

export default TaskList;
