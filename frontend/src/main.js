import { createApp } from 'vue';
import './style.scss';

import App from './App.vue';
import Todo from './components/Todo.vue';

const app = createApp(App);

app.component('Todo', Todo);

app.mount('#app');
