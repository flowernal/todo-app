<template>
    <div class="create-todo">
        <div class="title-input">
            <label for="title">Title</label>
            <input type="text" id="title" v-model="title" />
        </div>

        <div class="select-input-wrapper">
            <label for="state">State</label>
            <div class="select-input">
                <select id="state" v-model.number="state">
                    <option value="1">Open</option>
                    <option value="2">In-Progress</option>
                    <option value="3">Finished</option>
                </select>
            </div>
        </div>

        <div class="content-input">
            <label for="content">Content</label>
            <input type="text" id="content" v-model="content" />
        </div>

        <button class="success" @click="createData" :disabled="loading">
            {{ loading ? 'Loading...' : 'Create' }}
        </button>

        <p class="error" v-if="error">Error: {{ error.message }}</p>
    </div>
</template>

<script>
import { createTodo } from '../api';

export default {
    data() {
        return {
            loading: false,
            error: null,
            title: '',
            state: "1",
            content: '',
        }
    },
    methods: {
        async createData() {
            this.error = null;
            this.loading = true;

            try {
                this.todos = await createTodo({
                    title: this.title,
                    state: Number(this.state),
                    content: this.content,
                });
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
                alert("Todo was created successfully!");

                this.title = '';
                this.state = "1";
                this.content = '';
            }
        },
    },
}
</script>
