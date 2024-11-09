export const getTodos = async () => {
    const res = await fetch('/api/todo');
    const data = await res.json();

    if (data.isError) {
        throw data;
    }

    return data;
}

export const getTodo = async (id) => {
    const res = await fetch(`/api/todo/${id}`);
    const data = await res.json();

    if (data.isError) {
        throw data;
    }

    return data;
}