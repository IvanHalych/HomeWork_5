CREATE or replace FUNCTION insert_user(name_first varchar(128),
name_last varchar(128)) returns
    TABLE(id int  , first_name varchar(128),last_name varchar(128))
AS
    $$
    begin
        INSERT into users(first_name, last_name)  VALUES ($1,$2);
        return QUERY SELECT users.id,users.first_name,users.last_name
        from users where users.id = (select max(users.id) from users);
    end;
    $$
LANGUAGE plpgsql;

