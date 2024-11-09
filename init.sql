CREATE TABLE IF NOT EXISTS todos (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    title TEXT NOT NULL,
    state SMALLINT NOT NULL,
    content TEXT NOT NULL
);
