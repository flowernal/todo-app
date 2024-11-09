<template>
    <div class="todo">
        <select v-model.number="todo.state">
            <option value="1">Open</option>
            <option value="2">In-Progress</option>
            <option value="3">Finished</option>
        </select>
        <h2>{{ todo.title }}</h2>
    </div>
</template>

<script>
    export default {
        props: ['todo'],
    }
</script>
