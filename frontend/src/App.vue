<template>
    <h1>Todo App</h1>

    <div class="todos" v-for="todo in todos">
        <Todo :todo="todo" />
    </div>
</template>

<script>
import { getTodos } from './api';

export default {
    data() {
        return {
            loading: false,
            todos: null,
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
                this.todos = await getTodos();
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
            }
        }
    },
}
</script>
