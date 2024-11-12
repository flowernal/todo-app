<template>
    <div class="todo">
        <div class="todo-state-select">
            <div class="select-input">
                <select v-model.number="todo.state" @change="updateData($event)">
                    <option value="1">Open</option>
                    <option value="2">In-Progress</option>
                    <option value="3">Finished</option>
                </select>
            </div>
        </div>

        <div class="todo-buttons">
            <button class="danger" @click="deleteData()">Delete</button>
        </div>

        <div class="todo-title">
            <p class="error" v-if="error">Error: {{ error.message }}</p>
            <h2 :class="{ 'strike': todo.state == 3 }">{{ todo.title }}</h2>
        </div>

        <div class="todo-content">
            <p :class="{ 'strike': todo.state == 3 }">{{ todo.content }}</p>
        </div>
    </div>
</template>

<script>
import { updateTodo, deleteTodo } from '../api';

export default {
    props: ['todo'],
    data() {
        return {
            loading: false,
            error: null,
        }
    },
    methods: {
        async updateData(event) {
            this.error = null;
            this.loading = true;

            try {
                await updateTodo(this.todo.id, {
                    state: Number(event.target.value),
                });

                this.$emit("updated", {
                    id: this.todo.id,
                    state: event.target.value,
                });
            } catch (error) {
                this.error = error;
            } finally {
                this.loading = false;
            }
        },
        
        async deleteData() {
            if (confirm("Do you really want to delete this todo?")) {
                this.error = null;
                this.loading = true;

                try {
                    await deleteTodo(this.todo.id);
                    this.$emit("deleted", this.todo.id);
                } catch (error) {
                    this.error = error;
                } finally {
                    this.loading = false;
                }
            }
        }
    },
}
</script>
