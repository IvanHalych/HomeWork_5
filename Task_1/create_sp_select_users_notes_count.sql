CREATE or replace FUNCTION select_users_notes_count() returns
    TABLE(id int, first_name varchar(124), last_name varchar(124), count bigint)
AS
    $$
    begin
        return QUERY
            SELECT users.id, users.first_name,users.last_name,COUNT(notes.id)
            from users left join notes  on users.id = notes.user_id
            where notes.is_deleted =false
            GROUP BY users.id;
    end
    $$
LANGUAGE plpgsql;