<template>
    <div class="home">
        <div class="select-input">
            <select v-model="state" @change="fetchData">
                <option value="all">All</option>
                <option value="created">Created</option>
                <option value="finished">Finished</option>
            </select>
        </div>

        <p class="error" v-if="error">Error: {{ error.message }}</p>

        <div class="todos">
            <p v-if="loading">Loading...</p>
            <p v-else-if="!todos || !todos.length">No todos found.</p>

            <Todo
                v-for="todo in todos"
                :key="todo.id"
                :todo="todo"
                @updated="fetchData"
                @deleted="removeTodo"
            />
        </div>
    </div>
</template>

<script>
import { getTodos } from '../api';

export default {
    data() {
        return {
            loading: false,
            todos: null,
            state: 'all',
            error: null,
        }
    },
    created() {
        this.fetchData();
    },
    methods: {
        async fetchData() {
            this.todos = this.error = null;
            this.loading = true;

            try {
                this.todos = await getTodos(this.state);
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
            }
        },
        removeTodo(id) {
            this.todos = this.todos.filter(todo => todo.id !== id);
        }
    },
}
</script>
