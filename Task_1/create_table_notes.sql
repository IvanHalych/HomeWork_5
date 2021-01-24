CREATE TABLE notes(
    id uuid PRIMARY KEY,
    header varchar(128) NOT NULL,
    body varchar(1024) NOT NULL,
    is_deleted boolean NOT NULL,
    user_id INT references users(id),
    modified_at TIMESTAMPTZ NOT NULL DEFAULT current_timestamp
     );
CREATE INDEX ON notes(modified_at);