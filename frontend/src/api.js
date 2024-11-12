export const getTodos = async (state) => {
    const res = await fetch(`/api/todo?state=${state}`);
    const data = await res.json();

    if (data.isError) {
        throw data.error;
    }

    return data;
}

export const getTodo = async (id) => {
    const res = await fetch(`/api/todo/${id}`);
    const data = await res.json();

    if (data.isError) {
        throw data.error;
    }

    return data;
}

export const createTodo = async (todo) => {
    const res = await fetch('/api/todo', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(todo),
    });

    if (res.ok) return;

    const data = await res.json();

    if (data.isError) {
        throw data.error;
    }
}

export const updateTodo = async (id, todo) => {
    const res = await fetch(`/api/todo/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(todo),
    });

    if (res.ok) return;

    const data = await res.json();

    if (data.isError) {
        throw data.error;
    }
}

export const deleteTodo = async (id) => {
    const res = await fetch(`/api/todo/${id}`, {
        method: 'DELETE',
    });

    if (res.ok) return;

    const data = await res.json();

    if (data.isError) {
        throw data.error;
    }
}