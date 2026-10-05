import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import TaskList from './TaskList';
import TaskInput from './TaskInput';

const Stack = createStackNavigator();

const Navigation = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="TaskList">
        <Stack.Screen name="TaskList" component={TaskList} />
        <Stack.Screen name="TaskInput" component={TaskInput} />
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default Navigation;
