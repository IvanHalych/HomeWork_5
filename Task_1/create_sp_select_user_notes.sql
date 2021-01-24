CREATE or replace FUNCTION select_user_notes(_id int) returns
    TABLE(id uuid  ,header varchar(128),body varchar(1024),
    is_deleted boolean , modified_at timestamptz,
    user_id int)
AS
    $$
    begin
        return QUERY
            SELECT notes.id,notes.header,notes.body,
                   notes.is_deleted,notes.modified_at,
                   notes.user_id
            from users left join notes on users.id = notes.user_id
            where users.id = $1 and notes.is_deleted = false;
    end
    $$
LANGUAGE plpgsql;