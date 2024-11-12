import { createApp } from 'vue';
import { createMemoryHistory, createRouter } from 'vue-router';
import './style.scss';

import App from './App.vue';
import Todo from './components/Todo.vue';

import HomeView from './views/HomeView.vue';
import CreateView from './views/CreateView.vue';

const routes = [
    { path: '/', component: HomeView },
    { path: '/create', component: CreateView },
];

const router = createRouter({
    history: createMemoryHistory(),
    routes,
});

const app = createApp(App);

app.use(router);

app.component('Todo', Todo);

app.mount('#app');
