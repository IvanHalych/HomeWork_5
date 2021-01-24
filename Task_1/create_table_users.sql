CREATE TABLE users(
    id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    first_name varchar(128) NOT NULL,
    last_name varchar(128) NOT NULL
);